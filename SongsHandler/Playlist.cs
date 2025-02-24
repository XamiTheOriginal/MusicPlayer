using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {
        private readonly JsonHandler _fileManager = new JsonHandler("PLaylists.json");
        private List<int> _songList;
        public int SongCount => _songList.Count;
        public bool IsEmpty => _songList.Count == 0;
        public string Name { get; set; }

        public Playlist(string name, List<int> songList)
        {
            Name = name;
            _songList = songList;
        }
        
        public Playlist(string name)
        {
            Name = name;
            _songList = _fileManager.LoadFromJson(name);
        }
        
        public List<int> GetSongList()
        {
            return _songList;
        }
        
        public void AddSong(int item)
        {
            _songList.Add(item);
        }

        public void RemoveSong(int item)
        {
            _songList.Remove(item);
        }

        public void Save()
        {
            _fileManager.SaveToJson(Name, _songList);
        }
    }
}
