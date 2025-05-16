using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MusicPlayer.SongsHandler;
using MusicPlayer.UI.ViewModels;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer.UI.Views;

public partial class SongSelectionWindow : Window
{
    private SongsManager _songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
    private PlaylistsManager _playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
    
    public SongSelectionWindow()
    {
        InitializeComponent();
        DataContext = new SongSelectionViewModel();
    }
    
    private void OnValidateSelectionClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string selectedPlaylist = PlaylistListBox.SelectedItem as string;
        List<String> selectedSongTitles = SongListBox.SelectedItems.Cast<string>().ToList();

        if (selectedPlaylist == null || selectedPlaylist == "Default" || selectedSongTitles.Count == 0)
        {
            // Ne rien faire si aucun des deux n'est sélectionné
            return;
        }

        // Tu peux ici faire ce que tu veux avec les éléments sélectionnés
        Console.WriteLine($"Playlist sélectionnée : {selectedPlaylist}");
        Console.WriteLine("Chansons sélectionnées :");
        SongListBox.SelectedItems.Clear();

        Playlist playlist = _playlistsManager.GetItemByTitle(selectedPlaylist);
        foreach (string song in selectedSongTitles)
        {
            Console.WriteLine(song);
            playlist.AddSong(_songsManager.GetItemByTitle(song).Id);
        }
    }
}