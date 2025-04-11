using System.Reflection;
using MusicPlayer;
using MusicPlayer.SongsHandler;
using Xunit;

namespace Test;

public class SongTest
{
    [Fact]
    public void AlbumTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal("",song.Album);
    }
    [Fact]
    public void ArtistTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal("ADEFINIR",song.Artist);
    }
    [Fact]
    public void DurationTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal(30,song.Duration);
    }
    [Fact]
    public void FilepathTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal("DATA/Musics/NeverGonna.mp3",song.Filepath);
    }
    [Fact]
    public void IdTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal(2,song.Id);
    }
    [Fact]
    public void MoodTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal(Moods.Chill,song.Mood);
    }
    [Fact]
    public void TitleTest()
    {
        Song song = new Song("DATA/Musics/NeverGonna.mp3", 2);
        Assert.Equal("ADEFINIR",song.Title);
    }
}