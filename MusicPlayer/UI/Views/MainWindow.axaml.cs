using System;
using System.Collections.Generic;
using AngleSharp.Common;
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
    
    private void UpdateSelectedSong()
    {
        var currentSong = Player.CurrentSongId; // Récupère l'ID de la chanson actuelle
        var songIndex = _songsManager.GetAllItems().FindIndex(song => song.Id == currentSong); // Trouve l'index de la chanson

        if (songIndex >= 0)  // Si l'index est valide
        {
            //TODO : Correct me 
            //SongsListBox.SelectedIndex = songIndex;  // Met à jour l'élément sélectionné dans la ListBox
        }
    }
    private void Button_Previous(object? sender, RoutedEventArgs e)
    {
        Player.PreviousSong();
        UpdateSelectedSong();
    }
    
    private void Button_Play(object? sender, RoutedEventArgs e)
    {
        Player.TogglePlayPause();
        UpdateSelectedSong();
    }
    
    private void Button_Next(object? sender, RoutedEventArgs e)
    {
        Player.NextSong();
        UpdateSelectedSong();
    }
    
    

    private async void AddSongFile(object? sender, RoutedEventArgs e)
    {
        //TODO: ajouter les sons dans les playlists en fonction de MainViewModel.SelectedPlaylist
        
        
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
                if (DataContext is MainViewModel vm && vm.SelectedPlaylist != null)
                {
                    int songId = _songsManager.TryGetItemByPath(file.Path.LocalPath).Id;
                    vm.SelectedPlaylist.SongList.Add(songId);
                }
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

    private async void AddSongToPlaylist(object? sender, RoutedEventArgs e)
    {
        Window prompt = new SongSelectionWindow();
        
        Console.WriteLine("Essaie de lancer SongSelectionWindow");
        
        await prompt.ShowDialog(this);
    }

    private void PlaylistsListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.SelectedPlaylistIndex = ((ListBox)sender).SelectedIndex;
        }
        
    }
    


    private void SongsListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel && sender is ListBox listBox)
        {
            viewModel.SelectedSongIndex = listBox.SelectedIndex;
        }
    }

}