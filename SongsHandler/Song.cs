using System;
using System.IO;
using TagLib;

namespace MusicPlayer.SongsHandler;


public class Song
{
    public int Id { get; set; }
    public string Filepath { get; set; }
    public string Title { get; set; }
    public string? Artist { get; set; }
    public string? Album { get; set; }
    
    public double Duration { get; set; }
    
    
    public Song(int id)
    {
        Id = id;
    }
    
    public Song(string filepath)
    {
        Filepath = filepath;
        ExtractMetadata();
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
            Title = Path.GetFileNameWithoutExtension(Filepath); // Fallback au nom du fichier
            Duration = 0;
        }
    }
}