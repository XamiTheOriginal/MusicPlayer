using System;
using System.IO;

namespace MusicPlayer
{
    public static class FileHelper
    {
        /// <summary>
        /// Retourne le chemin absolu du dossier de sauvegarde (ex: DATA) et le crée s'il n'existe pas.
        /// </summary>
        /// <param name="folderName">Nom du dossier à créer (par défaut : "DATA")</param>
        /// <returns>Chemin absolu du dossier</returns>
        public static string GetOrCreateSaveFolder(string folderName = "DATA")
        {
            string saveFolderPath = Path.Combine(AppContext.BaseDirectory, folderName);
            
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
                Console.WriteLine($"📁 Dossier de sauvegarde créé : {saveFolderPath}");
            }

            return saveFolderPath;
        }

        /// <summary>
        /// Retourne le chemin absolu d'un fichier de sauvegarde dans le dossier (ex: DATA/Songs.json)
        /// et s'assure que le dossier existe.
        /// </summary>
        /// <param name="filename">Nom du fichier (ex: "Songs.json")</param>
        /// <param name="folderName">Nom du dossier (par défaut : "DATA")</param>
        /// <returns>Chemin absolu complet du fichier</returns>
        public static string GetSaveFilePath(string filename, string folderName = "DATA")
        {
            string folderPath = GetOrCreateSaveFolder(folderName);
            return Path.Combine(folderPath, filename);
        }
    }
}