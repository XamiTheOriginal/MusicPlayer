using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using LibVLCSharp.Shared;

namespace MusicPlayer
{
    public class Player
    {
        #region ClassVariables

        public int CurrentSongId;
        private Queue<int> _nextSongIdQueue = new Queue<int>();
        private Queue<int> _previousSongQueue = new Queue<int>();

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
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

        public Player()
        {
            // Spécifiez le chemin complet vers libvlc.dylib
            
    
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            CurrentSongId = 1;
        }

        public void SetCurrentSongId(int songId)
        {
            CurrentSongId = songId;
            PlayDaMusic(); // Lance la chanson immédiatement après la mise à jour de l'ID
        }

        public string GetFilePath()
        {
            return CurrentSong.Filepath;
        }

        public void PlayDaMusic()
        {
            try
            {
                if (_mediaPlayer.IsPlaying)
                {
                    PauseDaMusic();
                    return;
                }

                var media = new Media(_libVLC, CurrentSong.Filepath, FromType.FromPath);
                _mediaPlayer.Play(media);
                _isPlaying = true;

                _mediaPlayer.EndReached += (sender, e) => {
                    NextSong();
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la lecture : {e.Message}");
            }
        }

        public void PauseDaMusic()
        {
            if (_mediaPlayer != null && _isPlaying)
            {
                _mediaPlayer.Pause();
                _isPlaying = false;
            }
        }

        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
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
