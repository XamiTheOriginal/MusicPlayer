using System;
using System.IO;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer.SongsHandler;

public class Song
{
    public int Id { get; set; }
    public string Filepath { get; set; }
    public string? Title { get; set; }
    public string? Artist { get; set; }
    public string? Album { get; set; }
    
    public double Duration { get; set; }
    public Moods Mood { get; set; }

    private readonly PlaylistsManager? _playlistsManager;

    public Song(string filepath, int id, PlaylistsManager? playlistsManager = null)
    {
        Filepath = filepath;
        Id = id;
        Mood = Moods.Neutral;
        _playlistsManager = playlistsManager;
        ExtractMetadata();
        PlaylistSetup();
    }

    private void PlaylistSetup()
    {
        if (_playlistsManager is null)
            return; // en test unitaire, ou si on n’en a pas besoin

        if (Artist is not null)
        {
            var temp = _playlistsManager.GetItemByName(Artist);
            temp?.AddSong(Id);
        }

        if (Album is not null)
        {
            var temp = _playlistsManager.GetItemByName(Album);
            temp?.AddSong(Id);
        }
    }

    private void ExtractMetadata()
    {
        try
        {
            var file = TagLib.File.Create(Filepath);
            Title = string.IsNullOrEmpty(file.Tag.Title) ? Path.GetFileNameWithoutExtension(Filepath) : file.Tag.Title;
            Artist = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : null;
            Album = string.IsNullOrEmpty(file.Tag.Album) ? null : file.Tag.Album;
            Duration = file.Properties.Duration.TotalSeconds;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'extraction des métadonnées : {ex.Message}");
            Title = Path.GetFileNameWithoutExtension(Filepath); // fallback
            Duration = 0;
        }
    }
}