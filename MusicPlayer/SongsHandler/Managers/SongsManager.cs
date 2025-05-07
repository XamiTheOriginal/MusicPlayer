using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler;


namespace MusicPlayer.SongsHandler.Managers
{
    /// <summary>
    /// Gère la liste des chansons.
    /// </summary>
    public class SongsManager : BaseManager<Song>
    {
        /// <summary>
        /// Constructeur qui définit le chemin du fichier JSON.
        /// </summary>
        public SongsManager() : 
            base(Path.Combine(AppContext.BaseDirectory, "DATA", "Songs.json")) { }
        
        public Song? TryGetItemById(int id)
        {
            try { return GetItemById(id); }
            catch (KeyNotFoundException) { return null; }
        }

        public Song TryGetItemByPath(string path)
        {
            foreach (var song in GetAllItems())
            {
                if (song.Filepath == path)
                    return song;
            }
            throw new ArgumentException("mauvais filepath");
        }

        public new void AddItem(string filepath) 
        {
            Song item = new Song(filepath, GetNextId());
            if (!File.Exists(item.Filepath))
            {
                Console.WriteLine($"⚠️ Le fichier '{item.Filepath}' est introuvable. Ajout annulé.");
                return; // cas d'erreur
            }

            base.AddItem(item);
            
            ExtractMetadata(item);
            PlaylistSetup(item);

            if (ServiceLocator.Instance is null) throw new NullReferenceException("ServiceLocator est pas instancié.");
            var playlistsManager = ServiceLocator.Get<PlaylistsManager>();


            var defaultPlaylist = playlistsManager.GetItemByTitle("Default");
            

            if (defaultPlaylist != null)
            {
                defaultPlaylist.AddSong(item.Id);
            }
            else
            {
                Console.WriteLine("⚠️ Playlist 'Default' introuvable.");
            }
        }
        
        private void PlaylistSetup(Song item)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
         
            if (item.Artist is not null)
            {
                Playlist? temp = playlistsManager.GetItemByTitle(item.Artist);
                if (temp is not null) temp.AddSong(item.Id);
            }
            if (item.Album is not null)
            {
                Playlist? temp = playlistsManager.GetItemByTitle(item.Album);
                if (temp is not null) temp.AddSong(item.Id);
            }
        }
 
        private void ExtractMetadata(Song item)
        {
            try
            {
                var file = TagLib.File.Create(item.Filepath);

                item.Title = string.IsNullOrEmpty(file.Tag.Title) ? Path.GetFileNameWithoutExtension(item.Filepath) : file.Tag.Title;
                item.Artist = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : null;
                item.Album = string.IsNullOrEmpty(file.Tag.Album) ? null : file.Tag.Album;
                item.Duration = file.Properties.Duration.TotalSeconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'extraction des métadonnées : {ex.Message}");
                item.Title = Path.GetFileNameWithoutExtension(item.Filepath);
                item.Duration = 0;
            }
        }
    }
}
