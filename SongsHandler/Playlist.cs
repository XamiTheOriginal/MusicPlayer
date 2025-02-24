using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer
{
    public class Playlist
    {
        private JsonHandler _fileManager = new JsonHandler("PLaylists.json");
        public List<int>? SongList { get; set; }
        public string? Name { get; set; }

        public Playlist() { }

        public void CreatePLaylist(List<int>? list, string name)
        {
            SongList = list ?? new List<int>();
            Name = name;
        }

        public void LoadPLaylist(string name)
        {
            
        }

 
        
        

        public void Add(int item)
        {
            SongList.Add(item);
        }

        public void Remove(int item)
        {
            SongList.Remove(item);
        }

        public void Save()
        {
            _fileManager.SaveToJson(Name, SongList);
        }
       
    }
}
