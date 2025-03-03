using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.SongsHandler.Managers
{
    /// <summary>
    /// Gère la liste des playlists.
    /// </summary>
    public class PlaylistsManager : BaseManager<Playlist>
    {
        public PlaylistsManager() 
            : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATA", "PlayLists.json")) { }
    }
}
