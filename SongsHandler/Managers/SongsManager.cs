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

        public new void AddItem(Song item) 
        {
            base.AddItem(item);
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            playlistsManager.GetItemByName("Default").AddSong(item.Id);
            
        }
    }
}
