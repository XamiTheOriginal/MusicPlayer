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

    protected void LoadState()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json) ?? new SaveData();
            ItemsList = saveData.Items;
            AvailableIds = new Queue<int>(saveData.AvailableIds);
            NextId = saveData.NextId;
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

/// <summary>
/// Gère la liste des chansons.
/// </summary>
public class SongsManager : BaseManager<Song>
{
    public SongsManager() : base("\\DATA\\Songs.json") {}
}

/// <summary>
/// Gère la liste des playlists.
/// </summary>
public class PlaylistsManager : BaseManager<Playlist>
{
    public PlaylistsManager() : base("\\DATA\\Playlists.json") {}
}


public static class DependencyInjection
{
    public static void AddMusicManagers(this ServiceCollection services)
    {
        services.AddSingleton<SongsManager>();
        services.AddSingleton<PlaylistsManager>();
    }
}
