using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

            string path = Path.Combine("DATA", "Musics", "Linkin Park \u29f8 Slipknot \u29f8 Eminem - Damage.mp3");
            songsManager.AddItem(path);

            var defaultPlaylist = playlistsManager.GetItemByTitle("Default");
            if (defaultPlaylist != null) throw new Exception("Default playlist null");
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}