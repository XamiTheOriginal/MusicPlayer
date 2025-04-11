using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;

namespace MusicPlayer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new UI.Views.MainWindow();
            desktop.Exit += OnExit;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
        var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();

        playlistsManager.SaveState();
        songsManager.SaveState();
    }
}