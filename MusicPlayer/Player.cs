using System;
using System.Collections.Generic;
using MusicPlayer.SongsHandler;
using NAudio.Wave;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;


namespace MusicPlayer
{
    
    //faire un singleton avec le player comme les managers
    public class Player
    {
        #region ClassVariables

        public int CurrentSongId;
        private Queue<int> _nextSongIdQueue = new Queue<int>();
        private Queue<int> _previousSongQueue = new Queue<int>();
        
        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;
        private bool _isPlaying;
        private Song CurrentSong
        {
            get
            {
                var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
                return songsManager.GetItemById(CurrentSongId);
            }
        }

        #endregion
        
        public Player(WaveOutEvent outputDevice, AudioFileReader audioFile)
        {
            _outputDevice = outputDevice;
            _audioFile = audioFile;
            CurrentSongId = 1;
        }

        public string GetFilePath()
        {
            return CurrentSong.Filepath;
        }
        
        public void PlayDaMusic()
        {
            try
            {
                // Si déjà en train de jouer, on ne relance pas
                if (_isPlaying)
                    return;

                // Libération des anciens objets s'ils existent
                _audioFile?.Dispose();
                _outputDevice?.Dispose();

                // Chargement du nouveau fichier
                _audioFile = new AudioFileReader(CurrentSong.Filepath);
                _outputDevice = new WaveOutEvent();

                // Gestion de fin automatique
                _outputDevice.PlaybackStopped += (s, e) =>
                {
                    // Vérifie que la musique est bien terminée (et pas juste "pause")
                    if (_audioFile.Position >= _audioFile.Length)
                    {
                        NextSong();
                    }
                };

                _outputDevice.Init(_audioFile);
                _outputDevice.Play();
 
                _isPlaying = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la lecture : {e.Message}");
            }
        }

        public void PauseDaMusic()
        {
            if (_outputDevice != null && _isPlaying)
            {
                _outputDevice.Pause();
                _isPlaying = false;
            }
        }

            
        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            //var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
            Playlist playlist = playlistsManager.GetItemById(id);
            _nextSongIdQueue = new Queue<int>(playlist.SongList);
            _previousSongQueue = new Queue<int>();
            if (_nextSongIdQueue.Count > 0)
            {
                NextSong();
            }
            else
            {
                Console.WriteLine("La playlist est vide.");
            }

        }

        public void NextSong()
        {
            _previousSongQueue.Enqueue(CurrentSongId);
            CurrentSongId = _nextSongIdQueue.Dequeue();
            PlayDaMusic();
        }

        public void PreviousSong()
        {
            _nextSongIdQueue.Enqueue(CurrentSongId);
            CurrentSongId = _previousSongQueue.Dequeue();
            PlayDaMusic();
        }
    }
    
}
