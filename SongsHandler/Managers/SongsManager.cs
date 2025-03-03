using System;
using System.IO;
using System.Text.Json;

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
        public SongsManager() : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "DATA", "Songs.json"))
        {
        }

        /// <summary>
        /// Initialise les données par défaut si le fichier JSON n'existe pas ou est invalide.
        /// </summary>
        protected override void InitializeDefaultData()
        {
            AddItem(new Song("C:\\Musique\\Chanson1.mp3", NextId));
            AddItem(new Song("C:\\Musique\\Chanson2.mp3", NextId));
            SaveState(); // On sauvegarde pour créer le fichier JSON
        }
    }
}
