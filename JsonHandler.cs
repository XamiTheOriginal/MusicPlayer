using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPlayer
{
    public class JsonHandler
    {
        private readonly string _filePath;

        public JsonHandler(string filePath)
        {
            _filePath = "DATA\\" + filePath;
        }

        // Sauvegarde une liste avec un nom (sans écraser les autres)
        public void SaveToJson(string name, List<int> numbers)
        {
            Dictionary<string, List<int>> data = LoadAll();

            data[name] = numbers; // Ajoute ou met à jour la liste

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        // Charge une liste spécifique
        public List<int> LoadFromJson(string name)
        {
            Dictionary<string, List<int>> data = LoadAll();
            bool temp = data.TryGetValue(name, out var numbers);
            if (!temp) throw new Exception("No data found for " + name);
            return numbers;
        }

        // Charge toutes les données du fichier (ou crée un dictionnaire vide)
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