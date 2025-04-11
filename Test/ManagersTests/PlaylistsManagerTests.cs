using MusicPlayer.SongsHandler.Managers;
using System;
using System.IO;
using Xunit;

public class PlaylistsManagerTests
{
    private readonly string _testFilePath;

    public PlaylistsManagerTests()
    {
        string testDirectory = Path.Combine(AppContext.BaseDirectory, "TEST_DATA");
        Directory.CreateDirectory(testDirectory);
        _testFilePath = Path.Combine(testDirectory, "Playlists.json");

        // Assurer que l'ancien fichier est supprimé avant chaque test
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [Fact]
    public void LoadState_Should_CreateDefaultPlaylist_WhenFileMissing()
    {
        // Arrange
        var manager = new PlaylistsManagerTestable(_testFilePath);

        // Act
        manager.LoadState();

        // Assert
        var allPlaylists = manager.GetAllItems();
        Assert.Single(allPlaylists);
        Assert.Equal("Default", allPlaylists[0].Title);
        Assert.True(allPlaylists[0].IsEmpty);
    }

    // Classe héritée pour permettre l'injection du chemin de test
    private class PlaylistsManagerTestable : PlaylistsManager
    {
        public PlaylistsManagerTestable(string testPath)
            : base()
        {
            SaveFilePath = testPath;
        }
    }
}