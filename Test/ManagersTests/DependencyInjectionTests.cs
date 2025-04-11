using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;
using Xunit;
using System.IO;

public class DependencyInjectionTests
{
    [Fact]
    public void Managers_Are_Resolvable_From_DI()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMusicManagers();

        var provider = services.BuildServiceProvider();

        // Act
        var songsManager = provider.GetService<SongsManager>();
        var playlistsManager = provider.GetService<PlaylistsManager>();

        // Assert
        Assert.NotNull(songsManager);
        Assert.NotNull(playlistsManager);
    }
}