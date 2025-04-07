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
            string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATA", "Playlists.json");
    
            if (!File.Exists(dataFilePath))
            {
                Console.WriteLine($"⚠️ Le fichier Playlists.json est introuvable : {dataFilePath}");
            }

            return dataFilePath;
        }
        
    }
}