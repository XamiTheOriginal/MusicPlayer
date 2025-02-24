using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {
        private readonly string _filePath = "\\DATA\\Playlists.json" ;
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
            _songList = LoadFromJson(name);
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
            SaveToJson(Name, _songList);
        }
        
        private void SaveToJson(string name, List<int> numbers)
        {
            Dictionary<string, List<int>> data = LoadAll();

            data[name] = numbers; // Ajoute ou met à jour la liste

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        
        private List<int> LoadFromJson(string name)
        {
            Dictionary<string, List<int>> data = LoadAll();
            bool temp = data.TryGetValue(name, out var numbers);
            if (!temp) throw new Exception("No data found for " + name);
            return numbers;
        }
        
        private Dictionary<string, List<int>> LoadAll()
        {
            if (!File.Exists(_filePath))
                return new Dictionary<string, List<int>>();

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json) ??
                   new Dictionary<string, List<int>>();
        }
    }
}
