using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine($"🛠 Répertoire d'exécution : {AppContext.BaseDirectory}");
        
        var services = new ServiceCollection();
        
        services.AddMusicManagers(); // Ajout des managers au conteneur DI
        
        services.AddSingleton<Player>(); //Ajoute le singleton du player
        
        
        
        var provider = services.BuildServiceProvider();
        
        var songsManager = provider.GetRequiredService<SongsManager>();
        var playlistsManager = provider.GetRequiredService<PlaylistsManager>();
        
        ServiceLocator.Init(provider);
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }
    
    /* Pour récupérer songsManager et playlistsManager ailleurs :
    using Microsoft.Extensions.DependencyInjection;
    var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
    var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
    */
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}