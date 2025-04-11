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
}