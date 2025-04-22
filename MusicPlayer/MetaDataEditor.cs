using System;
using TagLib;
using MusicPlayer.SongsHandler;

namespace MusicPlayer;

public static class MetadataEditor
{
    /// <summary>
    /// Permet d'écrire à la main des MétaDatas dans un fichier (celui du song.filepath)
    /// </summary>
    /// <param name="song"></param>
    public static void WriteMetadata(Song song, string title = null, string performer = null,
        string album = "", Moods mood = Moods.Chill)
    {
        var file = TagLib.File.Create(song.Filepath);

        // Métadonnées standards
        //file.Tag.Title = song.Title ?? string.Empty;
        //file.Tag.Performers = new[] { song.Artist ?? string.Empty };
        //file.Tag.Album = song.Album ?? string.Empty;

        file.Tag.Title = title;
        file.Tag.Performers = new[] { performer };
        file.Tag.Album = album;
        // Champ "Mood" encodé dans Comment (en texte lisible)
        file.Tag.Comment = $"Mood:{mood}";

        file.Save();
    }

    public static void ReadMetadata(Song song)
    {
        var file = TagLib.File.Create(song.Filepath);
        var tag = file.Tag;

        song.Title = string.IsNullOrWhiteSpace(tag.Title) ? "Unknown" : tag.Title;
        song.Artist = (tag.Performers.Length > 0 && !string.IsNullOrWhiteSpace(tag.Performers[0])) ?
            tag.Performers[0] : "Unknown";
        song.Album = string.IsNullOrWhiteSpace(tag.Album) ? "Unknown" : tag.Album;

        Console.WriteLine(tag.Album);
        foreach (var chanteur in tag.Performers)
        {
            Console.WriteLine(chanteur);    
        }
        Console.WriteLine(tag.Title);
        
        // Récupérer le Mood dans le champ Comment
        if (!string.IsNullOrWhiteSpace(tag.Comment) && tag.Comment.StartsWith("Mood:"))
        {
            string moodStr = tag.Comment.Substring(5).Trim(); 
            //Récupère le Mood, le substring permet de remove le Mood: de la string
            if (Enum.TryParse<Moods>(moodStr, out var moodParsed))
            {
                song.Mood = moodParsed;
                Console.WriteLine(tag.Comment);
            }
        }
    }
}