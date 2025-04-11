using System.Reflection;
using MusicPlayer.SongsHandler;
using Xunit;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal("DATA/Musics/NeverGonna.mp3",song.Filepath);
    }
}