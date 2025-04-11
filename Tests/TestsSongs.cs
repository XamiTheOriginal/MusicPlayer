using MusicPlayer.SongsHandler;

namespace MusicPlayer.Tests;
using Xunit;

public class TestsSongs
{
    [Fact]
    public void TestFilePath()
    {
        Song song = new Song("/DATA/Musics/NeverGonna.mp3", 1);
        Assert.Equal("/DATA/Musics/NeverGonna.mp3",song.Filepath);
    }

    [Fact]
    public void TestTitle()
    {
        Song song = new Song("/DATA/Musics/NeverGonna.mp3", 1);
        Assert.Equal("/DATA/Musics/NeverGonna.mp3",song.Filepath);
    }
}