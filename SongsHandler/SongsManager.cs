namespace MusicPlayer.SongsHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// Gère la liste des chansons, leur ajout, suppression et la sauvegarde de leur état.
/// </summary>
public class SongsManager
{
    private List<Song> _songsList = new List<Song>();
    private Queue<int> _availableIds = new Queue<int>();
    private int _nextId = 1;
    private const string SaveFilePath = "\\DATA\\SongsManager.json";
    public Song GetSong(int id) => _songsList[id] != null ? _songsList[id] : throw new KeyNotFoundException();
    
    /// <summary>
    /// Initialise le gestionnaire de chansons et charge l'état sauvegardé si disponible.
    /// </summary>
    public SongsManager()
    {
        LoadState();
    }

    
    /// <summary>
    /// Ajoute une nouvelle chanson à la liste et lui assigne un ID unique.
    /// </summary>
    /// <param name="filepath">Emplacement du fichier de la chanson à ajouter.</param>
    /// <returns>ID de la chanson ajoutée.</returns>
    public int AddObject(string filepath)
    {
        int id;
        if (_availableIds.Count > 0) id = _availableIds.Dequeue(); // Prendre un ID libre
        else id = _nextId++;

        var obj = new Song(filepath, id);
        _songsList.Add(obj);
        SaveState(); // Sauvegarder l'état après l'ajout
        return id;
    }

    /// <summary>
    /// Supprime une chanson de la liste en fonction de son ID.
    /// </summary>
    /// <param name="id">L'ID de la chanson à supprimer.</param>
    /// <returns>Vrai si la chanson a été supprimée, faux si l'ID est introuvable.</returns>
    public bool RemoveObject(int id)
    {
        var obj = _songsList.FirstOrDefault(o => o.Id == id);
        if (obj != null)
        {
            _songsList.Remove(obj);
            _availableIds.Enqueue(id); // Réutiliser l'ID plus tard
            SaveState();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sauvegarde l'état actuel de la liste des chansons dans un fichier JSON.
    /// </summary>
    private void SaveState()
    {
        var state = new SaveData
        {
            Songs = _songsList,
            AvailableIds = _availableIds.ToList(),
            NextId = _nextId
        };

        string json = JsonConvert.SerializeObject(state, Formatting.Indented);
        File.WriteAllText(SaveFilePath, json);
    }

    /// <summary>
    /// Charge l'état sauvegardé de la liste des chansons depuis un fichier JSON.
    /// </summary>
    private void LoadState()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            var state = JsonConvert.DeserializeObject<SaveData>(json);
            if (state != null)
            {
                _songsList = state.Songs ?? new List<Song>();
                _availableIds = new Queue<int>(state.AvailableIds ?? new List<int>());
                _nextId = state.NextId;
            }
        }
    }
}


/// <summary>
/// Contient les données sauvegardées pour restaurer l'état du gestionnaire de chansons.
/// </summary>
public class SaveData
{
    public List<Song> Songs { get; set; }
    public List<int> AvailableIds { get; set; }
    public int NextId { get; set; }
}
