using System.Reflection;
using MusicPlayer;
using MusicPlayer.SongsHandler;
using Xunit;

namespace Test;

public class SongTest
{
    [Fact]
    public void Constructor_InitializesFilepathAndId()
    {
        var song = new Song("DATA/Musics/NeverGonna.mp3", 1);

        Assert.Equal("DATA/Musics/NeverGonna.mp3", song.Filepath);
        Assert.Equal(1, song.Id);
    }

    [Fact]
    public void Constructor_DefaultMoodIsNeutral()
    {
        var song = new Song("dummy.mp3", 0);

        Assert.Equal(Moods.Neutral, song.Mood);
    }

    [Fact]
    public void CanSetAndGet_Title_Artist_Album_Duration()
    {
        var song = new Song("dummy.mp3", 0)
        {
            Title = "Test Title",
            Artist = "Test Artist",
            Album = "Test Album",
            Duration = 42.5
        };

        Assert.Equal("Test Title", song.Title);
        Assert.Equal("Test Artist", song.Artist);
        Assert.Equal("Test Album", song.Album);
        Assert.Equal(42.5, song.Duration);
    }

    [Fact]
    public void CanChangeMood()
    {
        var song = new Song("dummy.mp3", 0);
        song.Mood = Moods.Chill;

        Assert.Equal(Moods.Chill, song.Mood);
    }
}