using Microsoft.Extensions.DependencyInjection;

namespace MusicPlayer.SongsHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


/// <summary>
/// Classe de base pour gérer des objets avec un système d'ID et de persistance.
/// </summary>
public abstract class BaseManager<T>
{
    protected List<T> ItemsList = new List<T>();
    protected Queue<int> AvailableIds = new Queue<int>();
    protected int NextId = 1;
    protected string SaveFilePath;

    private class SaveData
    {
        public List<T> Items { get; set; } = new List<T>();
        public List<int> AvailableIds { get; set; } = new List<int>();
        public int NextId { get; set; }
    }

    protected BaseManager(string saveFilePath)
    {
        SaveFilePath = saveFilePath;
        LoadState();
    }

    public void LoadState()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            Console.WriteLine($"Chargement du fichier {SaveFilePath} : {json}");

            var saveData = JsonConvert.DeserializeObject<SaveData>(json);
            if (saveData != null)
            {
                ItemsList = saveData.Items ?? new List<T>();
                AvailableIds = new Queue<int>(saveData.AvailableIds ?? new List<int>());
                NextId = saveData.NextId;
            }
            else
            {
                Console.WriteLine("Erreur : Le fichier JSON est corrompu, création d'une nouvelle liste.");
                ItemsList = new List<T>();
            }
        }
        else
        {
            Console.WriteLine($"Le fichier {SaveFilePath} n'existe pas, initialisation avec des valeurs par défaut.");
            ItemsList = new List<T>();
            InitializeDefaultData();
        }
    }

    protected virtual void InitializeDefaultData()
    {
        Console.WriteLine("Aucune donnée trouvée, ajout de valeurs par défaut...");
    }
    public void EnsureDataLoaded()
    {
        if (ItemsList.Count == 0)
        {
            Console.WriteLine("Aucune donnée trouvée, chargement des valeurs par défaut...");
            InitializeDefaultData();
        }
    }


    protected void SaveState()
    {
        var saveData = new SaveData
        {
            Items = ItemsList,
            AvailableIds = AvailableIds.ToList(),
            NextId = NextId
        };
        File.WriteAllText(SaveFilePath, JsonConvert.SerializeObject(saveData, Formatting.Indented));
    }

    public List<T> GetAllItems() => ItemsList;

    private int GenerateId()
    {
        return AvailableIds.Count > 0 ? AvailableIds.Dequeue() : NextId++;
    }

    public void AddItem(T item)
    {
        dynamic dynamicItem = item;
        dynamicItem.Id = GenerateId();
        ItemsList.Add(item);
        SaveState();
    }

    public void RemoveItem(T item)
    {
        dynamic dynamicItem = item;
        ItemsList.Remove(item);
        AvailableIds.Enqueue(dynamicItem.Id);
        SaveState();
    }

    public T GetItemById(int id)
    {
        dynamic foundItem = ItemsList.FirstOrDefault(item => ((dynamic)item).Id == id);
        return foundItem != null ? foundItem : throw new KeyNotFoundException();
    }
    
    public T? GetItemByName(string name)
    {
        dynamic foundItem = ItemsList.FirstOrDefault(item => ((dynamic)item).Name == name);
        return foundItem != null ? foundItem : null;
    }
}






