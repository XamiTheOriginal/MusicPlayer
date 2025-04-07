using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MusicPlayer.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Songs { get; } = new ObservableCollection<string>
        {
            "Chanson 1",
            "Chanson 2",
            "Chanson 3",
            "Chanson 1",
            "Chanson 2",
            "Chanson 3",
            "Chanson 1",
            "Chanson 2",
            "Chanson 3",
            "Chanson 1",
            "Chanson 2",
            "Chanson 3",
            "Chanson 1",
            "Chanson 2",
            "Chanson 3",
            "Chanson 1",
            "Chanson 2",
            "Chanson 3"
        };
        
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