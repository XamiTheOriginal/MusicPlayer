using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;


namespace MusicPlayer.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string>? Songs { get; }
        
        public MainViewModel()
        {
            var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();

            Songs = new ObservableCollection<string>();
            foreach (Song variableSong in songsManager.GetAllItems())
            {
                Songs.Add(variableSong.Title);
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