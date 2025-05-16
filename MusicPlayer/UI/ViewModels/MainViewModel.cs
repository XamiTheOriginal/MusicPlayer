using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using Avalonia.Threading;

namespace MusicPlayer.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region variables
        
        private SongsManager _songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
        private Player _player = ServiceLocator.Instance.GetRequiredService<Player>();
        private PlaylistsManager _playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
        public ObservableCollection<string> Songs { get; } = new();
        public ObservableCollection<string> Playlists { get; } = new();
        private DispatcherTimer _progressTimer;
        private Song _playingSong;
        public Song PlayingSong
        {
            get => _playingSong;
            set
            {
                _playingSong = value;
                OnPropertyChanged(nameof(PlayingSong));
                OnPropertyChanged(nameof(CurrentTitle));
                OnPropertyChanged(nameof(CurrentArtist));
                OnPropertyChanged(nameof(CurrentAlbumArt));
                OnPropertyChanged(nameof(CurrentLength));
                OnPropertyChanged(nameof(CurrentLengthFormatted));
                
                if (CurrentLength > 0)
                    Progress = _player.GetCurrentProgress().TotalSeconds / CurrentLength;
                else
                    Progress = 0;
            }
        }
        public string CurrentTitle => PlayingSong.Title;
        public string CurrentArtist => PlayingSong.Artist;
        public double CurrentLength => PlayingSong.Duration;

        public string CurrentLengthFormatted
        {
            get
            {
                int totalSeconds = (int)PlayingSong.Duration;
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;
                return $"{minutes:D2}:{seconds:D2}";
            }
        }
        
        public Bitmap? CurrentAlbumArt => AlbumArtHelper.GetAlbumArt(PlayingSong.Filepath);

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            private set
            {
                _selectedPlaylist = value;
                OnPropertyChanged(nameof(SelectedPlaylist));
            }
        }

        #endregion 

        public MainViewModel()
        {
            _playlistsManager.LoadState();

            PlayingSong = _songsManager.GetItemById(_player.CurrentSongId);
            _player.CurrentSongChanged += OnCurrentSongChanged; //abonnement à l'évènement "changement de playingsong"
            
            _progressTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _progressTimer.Tick += (sender, args) =>
            {
                if (CurrentLength > 0)
                {
                    Progress = _player.GetCurrentProgress().TotalSeconds / CurrentLength;
                }
            };
            _progressTimer.Start();
            
            Playlist defaultPlaylist = _playlistsManager.GetItemByTitle("Default");
            if (defaultPlaylist == null) 
                throw new Exception("Default playlist null");
            List<string> songList = defaultPlaylist.GetSongTitles();
            foreach (string song in songList)
            {
                Songs.Add(song);
            }

            List<Playlist> playlistList = _playlistsManager.GetAllItems();
            foreach (Playlist playlist in playlistList)
            {
                Playlists.Add(playlist.Title);
            }
            _selectedPlaylist = defaultPlaylist;
        }
        
        private int _selectedSongIndex;
        public int SelectedSongIndex
        {
            get => _selectedSongIndex;
            set
            {
                if (_selectedSongIndex != value)
                {
                    _selectedSongIndex = value;
                    OnPropertyChanged(nameof(SelectedSongIndex));
                    UpdateCurrentSongInPlayer(); // Mettez à jour le Player avec l'indice sélectionné
                }
            }
        }
        
        private int _selectedPlaylistIndex;
        public int SelectedPlaylistIndex
        {
            get => _selectedPlaylistIndex;
            set
            {
                _selectedPlaylistIndex = value;
                OnPropertyChanged(nameof(SelectedPlaylistIndex));
                UpdateSelectedPlaylist();
            }
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        private void OnCurrentSongChanged()
        {
            PlayingSong = _songsManager.GetItemById(_player.CurrentSongId);
        }
        
        private void UpdateCurrentSongInPlayer()
        {
            //TODO : Changer la selection dans l ui
            if (_selectedPlaylist != null && SelectedSongIndex >= 0)
            {
                
                _player.PlayFromPlaylist(_selectedPlaylist, SelectedSongIndex);
            }
        }
        
        private void UpdateSelectedPlaylist()
        {
            if (_selectedPlaylistIndex >= 0 && _selectedPlaylistIndex < _playlistsManager.GetAllItems().Count)
            {
                SelectedPlaylist = _playlistsManager.GetAllItems()[_selectedPlaylistIndex];
                RefreshSongs();
            }
        }

        public void RefreshPlaylists()
        {
            Console.WriteLine("Refreshing Playlists");
            Playlists.Clear();
            
            foreach (Playlist playlist in _playlistsManager.GetAllItems())
            {
                Playlists.Add(playlist.Title);
            }
        }

        public void RefreshSongs()
        {
            Console.WriteLine("Refreshing Songs");
            Songs.Clear();
            if (_selectedPlaylist?.SongList != null)
            {
                Console.WriteLine(_selectedPlaylist);
                foreach (var song in _selectedPlaylist.GetSongTitles())
                {
                    Songs.Add(song);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
