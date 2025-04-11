using TagLib;
using MusicPlayer.SongsHandler;

namespace MusicPlayer;

public static class MetadataEditor
{
    public static void WriteMetadata(Song song)
    {
        var file = TagLib.File.Create(song.Filepath);

        // Métadonnées standards
        file.Tag.Title = song.Title ?? string.Empty;
        file.Tag.Performers = new[] { song.Artist ?? string.Empty };
        file.Tag.Album = song.Album ?? string.Empty;

        // Champ "Mood" encodé dans Comment (en texte lisible)
        file.Tag.Comment = $"Mood:{song.Mood}";

        file.Save();
    }

    public static void ReadMetadata(Song song)
    {
        var file = TagLib.File.Create(song.Filepath);
        var tag = file.Tag;

        song.Title = string.IsNullOrWhiteSpace(tag.Title) ? "Unknown" : tag.Title;
        song.Artist = (tag.Performers.Length > 0 && !string.IsNullOrWhiteSpace(tag.Performers[0])) ? tag.Performers[0] : "ADEFINIR";
        song.Album = string.IsNullOrWhiteSpace(tag.Album) ? "Unknown" : tag.Album;

        // Récupérer le Mood dans le champ Comment
        if (!string.IsNullOrWhiteSpace(tag.Comment) && tag.Comment.StartsWith("Mood:"))
        {
            string moodStr = tag.Comment.Substring(5).Trim();
            if (Enum.TryParse<Moods>(moodStr, out var moodParsed))
            {
                song.Mood = moodParsed;
            }
        }
    }
}