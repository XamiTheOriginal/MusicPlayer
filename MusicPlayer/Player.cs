using System;
using System.Collections.Generic;
using System.Diagnostics;
using NAudio.Wave;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer
{
    public class Player
    {
        #region Variables

        public event Action? CurrentSongChanged;

        private Stopwatch _stopwatch = new Stopwatch();
        private Profiler _profiler = new Profiler();

        private int _currentSongId;
        public int CurrentSongId{
            get => _currentSongId;
            set
            {
                if (_currentSongId != value)
                {
                    _currentSongId = value;
                    CurrentSongChanged?.Invoke();
                    Console.WriteLine($"CurrentSongId changé : {_currentSongId}");
                }
            }
        }

        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        private bool _isPlaying;
        private bool _isPaused;

        private string _lastFilePath;

        private Playlist _currentPlaylist;
        private int _playlistIndex;

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
                _stopwatch.Stop();
                _profiler.UpdateData(CurrentSong.Mood, _stopwatch.Elapsed.Seconds);
                _stopwatch.Reset();
            }
            else
            {
                PlayDaMusic();
                _stopwatch.Start();
            }
        }

        public void SetCurrentSongId(int songId)
        {
            CurrentSongId = songId;
            _lastFilePath = ""; // force la relance même si le path est le même
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

                if (_isPaused && _outputDevice != null && currentPath == _lastFilePath)
                {
                    _outputDevice.Play();
                    _isPlaying = true;
                    _isPaused = false;
                    return;
                }

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

        #region Playlist / Navigation

        public void PlayFromPlaylist(Playlist playlist, int songIndex)
        {
            if (playlist == null || playlist.IsEmpty) return;

            _currentPlaylist = playlist;
            _playlistIndex = songIndex;
            SetCurrentSongId(_currentPlaylist.SongList[_playlistIndex]);
        }

        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            _currentPlaylist = playlistsManager.GetItemById(id);

            if (_currentPlaylist == null || _currentPlaylist.IsEmpty)
            {
                Console.WriteLine("Playlist vide ou introuvable.");
                return;
            }

            _playlistIndex = 0;
            SetCurrentSongId(_currentPlaylist.SongList[_playlistIndex]);
        }
        
        

        public void NextSong()
        {
            if (_currentPlaylist == null || _currentPlaylist.IsEmpty)
            {
                Console.WriteLine("Pas de playlist active. Lecture actuelle relancée.");
                PlayDaMusic(); // relancer la même chanson
                return;
            }

            _playlistIndex++;
            if (_playlistIndex >= _currentPlaylist.SongList.Count)
            {
                _playlistIndex = 0; // boucle
            }

            SetCurrentSongId(_currentPlaylist.SongList[_playlistIndex]);
        }

        public void PreviousSong()
        {
            if (_currentPlaylist == null || _currentPlaylist.IsEmpty)
            {
                Console.WriteLine("Pas de playlist active. Lecture actuelle relancée.");
                PlayDaMusic();
                return;
            }

            _playlistIndex--;
            if (_playlistIndex < 0)
            {
                _playlistIndex = _currentPlaylist.SongList.Count - 1; // boucle arrière
            }

            SetCurrentSongId(_currentPlaylist.SongList[_playlistIndex]);
        }

        #endregion
    }
}
