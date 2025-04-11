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
            : base(Path.Combine(AppContext.BaseDirectory, "DATA", "Playlists.json"))
        {
        }

        private static string GetDataFilePath()
        {

            
            string dataFilePath = Path.Combine(AppContext.BaseDirectory, "DATA", "Playlists.json");
            Console.WriteLine($"🔍 Chemin recherché : {dataFilePath}");
            Console.WriteLine($"📁 Fichier existe ? {File.Exists(dataFilePath)}");
            
            if (!File.Exists(dataFilePath))
            {
                Console.WriteLine($"⚠️ Le fichier Playlists.json est introuvable : {dataFilePath}");
            }

            return dataFilePath;
        }
        

    }
}