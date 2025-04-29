using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using MusicPlayer.UI.ViewModels;

namespace MusicPlayer.UI.Views;

public partial class MainWindow : Window
{
    
    private Player Player => ServiceLocator.Instance.GetRequiredService<Player>();
    private SongsManager _songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
    private PlaylistsManager _playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(); // Associe le ViewModel à la fenêtre

    }

    private void Button_Previous(object? sender, RoutedEventArgs e)
    {
        Player.PreviousSong();
    }
    
    private void Button_Play(object? sender, RoutedEventArgs e)
    {
        
        Player.PlayDaMusic();
    }
    
    private void Button_Next(object? sender, RoutedEventArgs e)
    {
        Player.NextSong();
    }

    private async void AddSongFile(object? sender, RoutedEventArgs e)
    {
        // Utiliser le StorageProvider pour ouvrir des fichiers
        IReadOnlyList<IStorageFile> files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choisissez des fichiers audio",
            AllowMultiple = true,
            FileTypeFilter = new[] 
            {
                new FilePickerFileType("Fichiers audio")
                {
                    Patterns = new[] { "*.mp3", "*.wav", "*.flac" },
                    MimeTypes = new[] { "audio/mpeg", "audio/wav", "audio/flac" }
                },
                new FilePickerFileType("Tous les fichiers")
                {
                    Patterns = new[] { "*.*" }
                }
            }
        });

        if (files.Count > 0)
        {
            foreach (var file in files)
            {
                _songsManager.AddItem(file.Path.LocalPath);
            }
        }
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.RefreshSongs();
        }
    }

    private async void AddPlaylist(object? sender, RoutedEventArgs e)
    {
        Window prompt = new NamePromptWindow();
        string? result = await prompt.ShowDialog<string?>(this); 

        if (!string.IsNullOrWhiteSpace(result))
        {
            _playlistsManager.AddItem(new Playlist(result, new List<int>())); 
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.RefreshPlaylists();
            }
        }
    }

    private void PlaylistsListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.RefreshSongs();
        }
    }

    private void SongsListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            
        }
    }
}