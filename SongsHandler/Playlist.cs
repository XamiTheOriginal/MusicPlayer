using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {
       
        public List<int> SongList;
        public int Id;
        public int SongCount => SongList.Count;
        public bool IsEmpty => SongList.Count == 0;
        public string Name { get; set; }
        
        public Playlist(string name, List<int> songList)
        {
            Name = name;
            SongList = songList;
        }
        
        public List<string> GetSongNames()
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

    }
}
