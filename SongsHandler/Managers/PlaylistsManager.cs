using System;
using System.Collections.Generic;
using System;
using System.IO;

namespace MusicPlayer.SongsHandler.Managers
{
    /// <summary>
    /// Gère la liste des playlists.
    /// </summary>
    public class PlaylistsManager : BaseManager<Playlist>
    {
        public PlaylistsManager()
            : base(GetDataFilePath())
        {
        }

        private static string GetDataFilePath()
        {
            // Récupère le dossier du projet en remontant depuis bin/Debug
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string dataFilePath = Path.Combine(projectDirectory, "DATA", "Playlists.json");

            // Vérifie si le fichier existe
            if (!File.Exists(dataFilePath))
            {
                Console.WriteLine($"⚠️ Le fichier Playlists.json est introuvable : {dataFilePath}");
            }

            return dataFilePath;
        }
    }
}