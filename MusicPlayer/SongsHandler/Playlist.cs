using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using MusicPlayer.SongsHandler.Managers;
using Newtonsoft.Json;


namespace MusicPlayer.SongsHandler
{
    public class Playlist
    {

        private SongsManager _songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();

        public List<int> SongList { get; set; } 
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
        
        public List<Song> GetSongs(SongsManager songsManager)
        {
            var validSongs = new List<Song>();

            foreach (int id in SongList)
            {
                var song = songsManager.TryGetItemById(id);
                if (song != null)
                {
                    validSongs.Add(song);
                }
                else
                {
                    Console.WriteLine($"⚠️ Chanson introuvable pour l'ID {id}");
                }
            }

            return validSongs;
        }

        
        
        public List<string> GetSongTitles()
        {
            List<string> names = new List<string>();

            // Vérifie si SongList est null avant de la parcourir
            if (SongList == null)
            {
                SongList = new List<int>(); // Initialise si c'est le cas
                Console.WriteLine("⚠️ SongList était null, initialisation d'une liste vide.");
            }

            foreach (var songId in SongList)
            {
                var song = _songsManager.TryGetItemById(songId);
                if (song != null)
                {
                    names.Add(song.Title);
                }
                else
                {
                    names.Add($"Chanson introuvable pour l'ID {songId}"); //Si la liste est vide
                }
            }

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
