using Avalonia;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.Downloader;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using LibVLCSharp.Shared;
using MusicPlayer;

namespace MusicPlayer;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // 1️⃣ Init console et dossier de sauvegarde
        Console.WriteLine($"🛠 Répertoire d'exécution : {AppContext.BaseDirectory}");
        FileHelper.GetOrCreateSaveFolder(AppContext.BaseDirectory +"DATA");

        // 2️⃣ Initialisation LibVLC
        Core.Initialize();

        // 3️⃣ Configuration DI
        var services = new ServiceCollection();
        services.AddMusicManagers();        // Managers
        services.AddSingleton<Player>();    // Player
        var provider = services.BuildServiceProvider();
        ServiceLocator.Init(provider);

        // 4️⃣ Chargement de l’état
        var songsManager = provider.GetRequiredService<SongsManager>();
        songsManager.LoadState();

        var playlistsManager = provider.GetRequiredService<PlaylistsManager>();
        playlistsManager.LoadState();

        // 5️⃣ Lancement de l'UI Avalonia
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}