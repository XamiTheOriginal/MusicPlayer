using MusicPlayer.SongsHandler;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer;

public class Helper
{
public static class SongLoader
{
    public static List<Song> LoadSongsFromDataFolder()
    {
        List<Song> loadedSongs = new();
        
        // Localise le dossier bin/Debug/... de l'exécutable
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Construit le chemin absolu vers DATA/Musics
        string musicFolder = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\DATA\Musics"));

        if (!Directory.Exists(musicFolder))
        {
            Console.WriteLine($"[Erreur] Dossier introuvable : {musicFolder}");
            return loadedSongs;
        }

        string[] mp3Files = Directory.GetFiles(musicFolder, "*.mp3");

        int id = 1;
        foreach (string file in mp3Files)
        {
            try
            {
                Song song = new Song(file, id++);
                loadedSongs.Add(song);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de chargement du fichier {file} : {ex.Message}");
            }
        }

        return loadedSongs;
    }
}

}