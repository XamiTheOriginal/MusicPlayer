using System.Collections.Generic;
using MusicPlayer.SongsHandler;
using Xunit;

namespace Test;

public class PlaylistTest
{
    [Fact]
    public void Constructor_InitializesCorrectly()
    {
        var songs = new List<int> { 1, 2, 3 };
        var playlist = new Playlist("MaPlaylist", songs);

        Assert.Equal("MaPlaylist", playlist.Title);
        Assert.Equal(3, playlist.SongCount);
        Assert.False(playlist.IsEmpty);
        Assert.Equal(songs, playlist.SongList);
    }
    
    [Fact]
    public void Constructor_NullSongList_InitializesEmptyList()
    {
        var playlist = new Playlist("Vide", null);

        Assert.Equal("Vide", playlist.Title);
        Assert.NotNull(playlist.SongList);
        Assert.Empty(playlist.SongList);
        Assert.True(playlist.IsEmpty);
    }
    
    [Fact]
    public void AddSong_AddsSongToList()
    {
        var playlist = new Playlist("Test", new List<int>());

        playlist.AddSong(42);

        Assert.Single(playlist.SongList);
        Assert.Equal(42, playlist.SongList[0]);
        Assert.False(playlist.IsEmpty);
    }
    
    [Fact]
    public void RemoveSong_RemovesExistingSong()
    {
        var playlist = new Playlist("Test", new List<int> { 1, 2, 3 });

        playlist.RemoveSong(2);

        Assert.DoesNotContain(2, playlist.SongList);
        Assert.Equal(2, playlist.SongCount);
    }
    
    [Fact]
    public void RemoveSong_RemovingNonExistingSong_ChangesNothing()
    {
        var playlist = new Playlist("Test", new List<int> { 1, 2, 3 });

        playlist.RemoveSong(42);

        Assert.Equal(3, playlist.SongCount);
        Assert.Contains(1, playlist.SongList);
        Assert.Contains(2, playlist.SongList);
        Assert.Contains(3, playlist.SongList);
    }
    
    [Fact]
    public void GetSongNames_ReturnsCorrectNames()
    {
        var playlist = new Playlist("Test", new List<int> { 10, 20 });

        var result = playlist.GetSongNames();

        Assert.Equal(new List<string> { "10", "20" }, result);
    }
    [Fact]
    public void SongCount_ReflectsActualCount()
    {
        var playlist = new Playlist("Test", new List<int>());

        Assert.Equal(0, playlist.SongCount);
        playlist.AddSong(5);
        Assert.Equal(1, playlist.SongCount);
    }

    [Fact]
    public void IsEmpty_ReturnsTrueWhenEmpty()
    {
        var playlist = new Playlist("Empty", new List<int>());
        Assert.True(playlist.IsEmpty);
    }

    [Fact]
    public void IsEmpty_ReturnsFalseWhenNotEmpty()
    {
        var playlist = new Playlist("NotEmpty", new List<int> { 1 });
        Assert.False(playlist.IsEmpty);
    }
}