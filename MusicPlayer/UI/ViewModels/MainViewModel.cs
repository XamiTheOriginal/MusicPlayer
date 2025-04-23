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
        public ObservableCollection<string> Songs { get; } = new();

        public MainViewModel()
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
            
            playlistsManager.LoadState();
            
            Console.WriteLine("📂 Playlists disponibles :");
            foreach (var p in playlistsManager.GetAllItems())
            {
                Console.WriteLine($"➡️ Playlist : {p.Title}, Songs: {p.SongList?.Count}");
            }
            
            var defaultPlaylist = playlistsManager.GetItemByTitle("Default");
            if (defaultPlaylist == null) 
                throw new Exception("Default playlist null");
            List<string> songList = defaultPlaylist.GetSongTitles();
            foreach (var song in songList)
            {
                Songs.Add(song);
            }
        }
        
        private int _selectedSongIndex;
        public int SelectedIndex
        {
            get => _selectedSongIndex;
            set
            {
                if (_selectedSongIndex != value)
                {
                    _selectedSongIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
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
            var player = ServiceLocator.Instance.GetRequiredService<Player>();
            player.SetCurrentSongId(_selectedSongIndex); // Envoie l'indice de la chanson au Player
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
