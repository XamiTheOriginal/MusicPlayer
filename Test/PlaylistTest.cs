using System.Collections.Generic;
using MusicPlayer.SongsHandler;
using Xunit;

namespace Test;

public class PlaylistTest
{
    [Fact]
    void IsEmptyTest()
    {
        Playlist playlist = new Playlist("test", new List<int>());
        Assert.Empty(playlist.SongList);
    }

    [Fact]
    void IsNotEmptyTest()
    {
        Playlist playlist = new Playlist("test", new List<int>(1));
        playlist.SongList.Add(1);
        Assert.NotEmpty(playlist.SongList);
    }
}