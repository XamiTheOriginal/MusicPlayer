using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SongsManager _songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
        private Player _player = ServiceLocator.Instance.GetRequiredService<Player>();
        private PlaylistsManager _playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
        public ObservableCollection<string> Songs { get; } = new();
        public ObservableCollection<string> Playlists { get; } = new();

        public MainViewModel()
        {
            _playlistsManager.LoadState();
            
            Console.WriteLine("📂 Playlists disponibles :");
            foreach (var p in _playlistsManager.GetAllItems())
            {
                Console.WriteLine($"➡️ Playlist : {p.Title}, Songs: {p.SongList?.Count}");
            }
            
            var defaultPlaylist = _playlistsManager.GetItemByTitle("Default");
            if (defaultPlaylist == null) 
                throw new Exception("Default playlist null");
            List<string> songList = defaultPlaylist.GetSongTitles();
            foreach (var song in songList)
            {
                Songs.Add(song);
            }

            List<Playlist> playlistList = _playlistsManager.GetAllItems();
            foreach (Playlist playlist in playlistList)
            {
                Playlists.Add(playlist.Title);
            }
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
        
        public int SelectedPlaylistIndex
        {
            get => _selectedSongIndex;
            set
            {
                if (_selectedSongIndex != value)
                {
                    _selectedSongIndex = value;
                    OnPropertyChanged(nameof(SelectedPlaylistIndex));
                    UpdateCurrentSongInPlayer(); // Mettez à jour le Player avec l'indice sélectionné
                }
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

        private void UpdateCurrentSongInPlayer()
        {
            
            _player.SetCurrentSongId(_selectedSongIndex); // Envoie l'indice de la chanson au Player
        }

        public void RefreshPlaylists()
        {
            Playlists.Clear();
            
            foreach (Playlist playlist in _playlistsManager.GetAllItems())
            {
                Playlists.Add(playlist.Title);
            }
        }

        public void RefreshSongs()
        {
            Songs.Clear();
            //TODO: implémenter une variable playlist actuelle ou qque chose du genre et afficher la liste de ses sons
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
