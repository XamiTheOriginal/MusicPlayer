using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {
       
        public List<int> SongList { get; } 
        public int Id;
        [JsonIgnore]
        public int SongCount => SongList.Count;
        [JsonIgnore]
        public bool IsEmpty => SongList.Count == 0;
        public string Title { get; set; }
        
        public Playlist(string title, List<int> songList)
        {
            Title = title;
            SongList = songList ?? new List<int>();
        }
        
        public List<string> GetSongTitles()
        {
            List<string> names = new List<string>();
            foreach (var song in SongList) names.Add(song.ToString());
            return names;
        }
        
        public void AddSong(int item)
        {
            SongList.Add(item);
        }

        public void RemoveSong(int item)
        {
            SongList.Remove(item);
        }

        public override string ToString() => Title;
    }
}
