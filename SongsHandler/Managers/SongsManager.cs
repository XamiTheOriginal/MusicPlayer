using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace MusicPlayer.SongsHandler.Managers
{
    /// <summary>
    /// Gère la liste des chansons.
    /// </summary>
    public class SongsManager : BaseManager<Song>
    {
        /// <summary>
        /// Constructeur qui définit le chemin du fichier JSON.
        /// </summary>
        public SongsManager() : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "DATA", "Songs.json"))
        {
        }
        
        public Song? TryGetItemById(int id)
        {
            try
            {
                return GetItemById(id);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }


        public new void AddItem(Song item) 
        {
            base.AddItem(item);
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            var defaultPlaylist = playlistsManager.GetItemByName("Default");

            if (defaultPlaylist != null)
            {
                defaultPlaylist.AddSong(item.Id);
            }
            else
            {
                Console.WriteLine("⚠️ Playlist 'Default' introuvable.");
            }

            
        }


    }
}
