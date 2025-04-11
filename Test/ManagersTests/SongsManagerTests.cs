using Xunit;
using MusicPlayer.SongsHandler.Managers;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer;
using System.Linq;

public class SongsManagerTests
{
    private readonly string _testSongsPath;
    private readonly string _testPlaylistsPath;

    public SongsManagerTests()
    {
        var baseDir = Path.Combine(AppContext.BaseDirectory, "TEST_DATA");
        Directory.CreateDirectory(baseDir);
        _testSongsPath = Path.Combine(baseDir, "Songs.json");
        _testPlaylistsPath = Path.Combine(baseDir, "Playlists.json");

        File.Delete(_testSongsPath);
        File.Delete(_testPlaylistsPath);

        var services = new ServiceCollection();
        services.AddSingleton(new PlaylistsManagerTestable(_testPlaylistsPath));
        ServiceLocator.Init(services.BuildServiceProvider());
    }

    [Fact]
    public void AddItem_Should_Add_Song_If_File_Exists()
    {
        // Arrange
        string dummyPath = Path.Combine(AppContext.BaseDirectory, "test.mp3");
        File.WriteAllText(dummyPath, "fake mp3 content"); // Simuler un fichier

        var manager = new SongsManagerTestable(_testSongsPath);

        // Act
        manager.AddItem(dummyPath);

        // Assert
        var songs = manager.GetAllItems();
        Assert.Single(songs);
        Assert.Equal(dummyPath, songs[0].Filepath);
    }

    [Fact]
    public void AddItem_Should_Not_Add_If_File_Not_Exists()
    {
        var manager = new SongsManagerTestable(_testSongsPath);
        manager.AddItem("nonexistent.mp3");

        Assert.Empty(manager.GetAllItems());
    }

    [Fact]
    public void Metadata_Should_Be_Extracted_If_Valid_File()
    {
        string dummyPath = Path.Combine(AppContext.BaseDirectory, "test_meta.mp3");
        File.WriteAllText(dummyPath, "fake mp3 content"); // Fichier bidon

        var manager = new SongsManagerTestable(_testSongsPath);
        manager.AddItem(dummyPath);

        var song = manager.GetAllItems().First();
        Assert.False(string.IsNullOrEmpty(song.Title));
        Assert.True(song.Duration >= 0);
    }

    [Fact]
    public void Playlist_Default_Should_Contain_Added_Song()
    {
        string dummyPath = Path.Combine(AppContext.BaseDirectory, "test_playlist.mp3");
        File.WriteAllText(dummyPath, "fake mp3 content");

        var manager = new SongsManagerTestable(_testSongsPath);
        manager.AddItem(dummyPath);

        var playlistManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
        var defaultPlaylist = playlistManager.GetItemByTitle("Default");

        Assert.NotNull(defaultPlaylist);
        Assert.Single(defaultPlaylist.SongList);
    }

    // Sous-classes pour injecter les chemins de test
    private class SongsManagerTestable : SongsManager
    {
        public SongsManagerTestable(string path)
        {
            SaveFilePath = path;
            LoadState();
        }
    }

    private class PlaylistsManagerTestable : PlaylistsManager
    {
        public PlaylistsManagerTestable(string path)
        {
            SaveFilePath = path;
            LoadState(); // Crée la playlist Default si absente
        }
    }
}
