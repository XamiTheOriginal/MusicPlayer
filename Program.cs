using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // D'abord on prépare la DI
        var services = new ServiceCollection();

        services.AddMusicManagers(); // Ajout des managers
        services.AddSingleton<Player>(); // Singleton du player

        var provider = services.BuildServiceProvider();
        ServiceLocator.Init(provider); // Initialisation du ServiceLocator avant tout

        // Maintenant on lance Avalonia (le DI est déjà prêt)
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}