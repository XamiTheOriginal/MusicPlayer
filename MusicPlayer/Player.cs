using System;
using NAudio.Wave;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer
{
    public class Player
    {
        #region Variables

        public int CurrentSongId;

        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        private bool _isPlaying;
        private bool _isPaused;
        
        private string _lastFilePath; // pour suivre la chanson réellement jouée


        private Song CurrentSong
        {
            get
            {
                var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
                return songsManager.GetItemById(CurrentSongId);
            }
        }

        public bool IsPlaying => _isPlaying;
        public bool IsPaused => _isPaused;

        #endregion

        #region Constructeur

        public Player(WaveOutEvent outputDevice)
        {
            _outputDevice = outputDevice;
            CurrentSongId = 1;
        }

        #endregion

        #region Méthodes principales

        public void TogglePlayPause()
        {
            if (_isPlaying)
            {
                PauseDaMusic();
            }
            else
            {
                PlayDaMusic();
            }
        }

        public void SetCurrentSongId(int songId)
        {
            CurrentSongId = songId;
            PlayDaMusic();
        }

        public string GetFilePath()
        {
            return CurrentSong.Filepath;
        }

        public void PlayDaMusic()
        {
            try
            {
                string currentPath = CurrentSong.Filepath;

                // Si en pause ET même fichier → reprise
                if (_isPaused && _outputDevice != null && currentPath == _lastFilePath)
                {
                    _outputDevice.Play();
                    _isPlaying = true;
                    _isPaused = false;
                    return;
                }

                // Sinon on relance la lecture proprement
                _audioFile?.Dispose();
                _outputDevice?.Dispose();

                _audioFile = new AudioFileReader(currentPath);
                _outputDevice = new WaveOutEvent();

                _outputDevice.PlaybackStopped += (s, e) =>
                {
                    if (!_isPaused && _audioFile.Position >= _audioFile.Length)
                    {
                        NextSong();
                    }
                };

                _outputDevice.Init(_audioFile);
                _outputDevice.Play();

                _isPlaying = true;
                _isPaused = false;
                _lastFilePath = currentPath;
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
                _isPaused = true;
                _isPlaying = false;
            }
        }

        #endregion

        #region Lecture enchaînée / Playlist

        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            Playlist playlist = playlistsManager.GetItemById(id);
            NextSong(); // À améliorer plus tard avec gestion réelle de playlist
        }

        public void NextSong()
        {
            // TODO : logique pour sélectionner la prochaine chanson dans une playlist
            PlayDaMusic();
        }

        public void PreviousSong()
        {
            // TODO : logique pour chanson précédente
            PlayDaMusic();
        }

        #endregion
    }
}
