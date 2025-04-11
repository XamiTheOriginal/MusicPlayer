using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;
using Newtonsoft.Json;

namespace MusicPlayer.SongsHandler;

[JsonObject(MemberSerialization.OptIn)]
public class Song
{
    [JsonProperty]
    public int Id { get; set; }
    
    [JsonProperty]
    public string Filepath { get; set; }
    
    [JsonProperty]
    public string? Title { get; set; }
    
    [JsonProperty]
    public string? Artist { get; set; }
    
    [JsonProperty]
    public string? Album { get; set; }
    
    [JsonProperty]
    public double Duration { get; set; }
    
    [JsonProperty]
    public Moods Mood { get; set; }

    [JsonIgnore]
    private readonly PlaylistsManager? _playlistsManager;

    public Song(string filepath, int id, PlaylistsManager? playlistsManager = null)
    {
        Filepath = filepath;
        Id = id;
        Mood = Moods.Neutral;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Title))
        {
            Title = Path.GetFileNameWithoutExtension(Filepath);
        }
        return Title;
    }
}