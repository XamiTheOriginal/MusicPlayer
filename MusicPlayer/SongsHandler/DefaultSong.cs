using MusicPlayer.SongsHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class DefaultSong : Song
    {
        public DefaultSong(string filepath, int id) : 
            base("C:\\Users\\maxim\\OneDrive\\Bureau\\C#\\MusicPlayer\\bin\\DATA\\Musics\\NeverGonna.mp3", -1)
        {
        }
    }
}
