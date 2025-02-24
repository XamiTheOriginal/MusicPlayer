using MusicPlayer.SongsHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MusicPlayer
{
    internal static class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddMusicManagers(); // Ajout des managers au conteneur DI
            
            var provider = services.BuildServiceProvider();
            ServiceLocator.Init(provider);
            var songsManager = provider.GetRequiredService<SongsManager>();
            var playlistsManager = provider.GetRequiredService<PlaylistsManager>();
            
            /* Pour récupérer songsManager et playlistsManager ailleurs :
            using Microsoft.Extensions.DependencyInjection;
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
            */
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}