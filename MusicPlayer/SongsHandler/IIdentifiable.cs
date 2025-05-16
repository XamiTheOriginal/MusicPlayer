namespace MusicPlayer.SongsHandler;

public interface IIdentifiable
{
    int Id { get; }
    string Title { get; }
}