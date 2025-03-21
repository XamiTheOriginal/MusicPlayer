using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {
       
        private List<int> _songList;
        public int Id;
        public int SongCount => _songList.Count;
        public bool IsEmpty => _songList.Count == 0;
        public string Name { get; set; }
        
        public Playlist(string name, List<int> songList)
        {
            Name = name;
            _songList = songList;
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

    }
}
