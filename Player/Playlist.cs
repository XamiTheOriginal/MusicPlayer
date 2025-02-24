using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer
{
    public class Playlist
    {
        public List<int> Queue { get; set; }
        public string Name { get; set; }

        public Playlist(List<int> queue, string name)
        {
            Queue = queue ?? new List<int>();
            Name = name;
        }

        public void Add(int item)
        {
            Queue.Add(item);
        }

        public void Remove(int item)
        {
            Queue.Remove(item);
        }

        // 🔹 Enregistrer la playlist en JSON
        public void SaveToFile(string filePath)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // 🔹 Charger une playlist depuis un fichier JSON
        public static Playlist LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ Fichier introuvable !");
                return null;
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Playlist>(json);
        }
    }
}
