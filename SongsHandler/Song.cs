namespace MusicPlayer.SongsHandler;

public class Song
{
    public int Id;
    public string Filepath;
    public string Title;
    public string? Artist;
    public string? Album;
    public string? Genre;

    public Song(string filepath, string title, int id)
    {
        Id = id;
        Filepath = filepath;
        Title = title;
        
    }
    
    
    
}