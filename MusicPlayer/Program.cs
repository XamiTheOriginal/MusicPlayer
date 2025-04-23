using Avalonia;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using NAudio.Wave;
using TagLib.Flac;

namespace MusicPlayer;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        /*string relativePath = @"..\..\..\DATA\Musics\Arcane S2 - Ma Meilleure Ennemie - Gragas AI Cover.mp3";
        string fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
        Song song = new Song(fullPath, 1);
        MetadataEditor.WriteMetadata(song, "Oui","Adele","feur",Moods.Dreamy);

        MetadataEditor.ReadMetadata(song);

        return;*/
        
        Console.WriteLine($"🛠 Répertoire d'exécution : {AppContext.BaseDirectory}");
        
        var services = new ServiceCollection();
        
        services.AddMusicManagers(); // Ajout des managers au conteneur DI
        services.AddSingleton<Player>(); //Ajoute le singleton du player
        
        services.AddTransient<WaveOutEvent>();
        
        var provider = services.BuildServiceProvider();
        
        var songsManager = provider.GetRequiredService<SongsManager>();
        songsManager.LoadState();
        var playlistsManager = provider.GetRequiredService<PlaylistsManager>();
        playlistsManager.LoadState();
        
        ServiceLocator.Init(provider);
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }
    
    /* Pour récupérer songsManager et playlistsManager ailleurs :
    using Microsoft.Extensions.DependencyInjection;
    var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
    var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
    var player = ServiceLocator.Instance.GetRequiredService<Player>();
    */
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}