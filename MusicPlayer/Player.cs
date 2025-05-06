using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using LibVLCSharp.Shared;

namespace MusicPlayer
{
    public class Player : IDisposable
    {
        #region ClassVariables

        public int CurrentSongId;
        private Queue<int> _nextSongIdQueue = new Queue<int>();
        private Stack<int> _previousSongStack = new Stack<int>();

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private Media? _currentMedia;
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
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            // Attacher l'événement EndReached une seule fois
            _mediaPlayer.EndReached += MediaPlayer_EndReached;

            CurrentSongId = 1;
        }

        private void MediaPlayer_EndReached(object? sender, EventArgs e)
        {
            NextSong();
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

                if (!File.Exists(CurrentSong.Filepath))
                {
                    Console.WriteLine($"⚠️ Fichier audio introuvable : {CurrentSong.Filepath}");
                    return;
                }

                Console.WriteLine($"Lecture de la chanson ID {CurrentSongId} : {CurrentSong.Title} - {CurrentSong.Filepath}");

                _currentMedia?.Dispose();

                _currentMedia = new Media(_libVLC, CurrentSong.Filepath, FromType.FromPath);
                _mediaPlayer.Media = _currentMedia;
                Console.WriteLine(CurrentSong.Filepath);

                _mediaPlayer.Play();
                _isPlaying = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la lecture : {e.Message}");
            }
        }

        public void PauseDaMusic()
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
                _isPlaying = false;
            }
        }

        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            Playlist playlist = playlistsManager.GetItemById(id);

            if (playlist.SongList == null || playlist.SongList.Count == 0)
            {
                Console.WriteLine("La playlist est vide.");
                return;
            }

            Console.WriteLine($"Playlist '{playlist.Title}' contient {playlist.SongList.Count} chansons.");

            var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
            foreach (var songId in playlist.SongList)
            {
                try
                {
                    var song = songsManager.GetItemById(songId);
                    Console.WriteLine($"Chanson trouvée : {song.Title} (ID {song.Id})");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"⚠️ Chanson introuvable pour l'ID {songId}");
                }
            }

            _nextSongIdQueue = new Queue<int>(playlist.SongList);
            _previousSongStack.Clear();

            NextSong();
        }

        public void NextSong()
        {
            if (_nextSongIdQueue.Count == 0)
            {
                Console.WriteLine("Fin de la playlist.");
                return;
            }

            _previousSongStack.Push(CurrentSongId);
            CurrentSongId = _nextSongIdQueue.Dequeue();
            PlayDaMusic();
        }

        public void PreviousSong()
        {
            if (_previousSongStack.Count == 0)
            {
                Console.WriteLine("Pas de chanson précédente.");
                return;
            }

            _nextSongIdQueue.Enqueue(CurrentSongId);
            CurrentSongId = _previousSongStack.Pop();
            PlayDaMusic();
        }

        public void Dispose()
        {
            _mediaPlayer?.Dispose();
            _currentMedia?.Dispose();
            _libVLC?.Dispose();
        }
    }
}
