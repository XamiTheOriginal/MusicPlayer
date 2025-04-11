using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.SongsHandler.Managers
{
    public static class DependencyInjection
    {
        public static void AddMusicManagers(this ServiceCollection services)
        {
            services.AddSingleton<SongsManager>();
            services.AddSingleton<PlaylistsManager>();
        }
    }
}
