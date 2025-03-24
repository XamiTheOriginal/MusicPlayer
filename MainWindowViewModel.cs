namespace MusicPlayer;

using System.Collections.ObjectModel;

public class MainWindowViewModel 
{
    public ObservableCollection<string> Songs { get; set; }

    public MainWindowViewModel()
    {
        Songs = new ObservableCollection<string>
        {
            "Chanson 1",
            "Chanson 2",
            "Chanson 3"
        };
    }
}