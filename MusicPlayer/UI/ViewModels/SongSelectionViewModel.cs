using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;



namespace MusicPlayer.UI.ViewModels;

public class SongSelectionViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private SongsManager _songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
    private PlaylistsManager _playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();

    public ObservableCollection<string> Songs { get; } = new();
    public ObservableCollection<string> Playlists { get; } = new();

    public SongSelectionViewModel()
    {
        Songs = new ObservableCollection<string>(_songsManager.GetAllTitles()) ;
        Playlists = new ObservableCollection<string>(_playlistsManager.GetAllTitles()) ;
    }
    
    
    
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    //useless but still here
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}