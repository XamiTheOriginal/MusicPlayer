using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.SongsHandler.Managers
{

    /// <summary>
    /// Gère la liste des chansons.
    /// </summary>
    public class SongsManager : BaseManager<Song>
    {
        public SongsManager() : base("\\DATA\\Songs.json") { }
    }
}
