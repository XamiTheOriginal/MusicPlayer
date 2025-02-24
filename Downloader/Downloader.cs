using System;
using System.IO;
using System.Linq;
using System.Net;
using VideoLibrary;
using VideoLibrary.Helpers;

public class Downloader
{

    public static void DownloadAudio(string videoUrl, string outputPath)
    {
        try
        {
            var youtube = YouTube.Default;
            var video = youtube.GetAllVideos(videoUrl)
                               .Where(v => v.AudioFormat != null) // Sélectionne uniquement l'audio
                               .OrderByDescending(v => v.AudioBitrate) // Meilleure qualité d'abord
                               .FirstOrDefault();

            if (video == null)
            {
                Console.WriteLine("Aucune piste audio trouvée.");
                return;
            }

            string filePath = Path.Combine(outputPath, video.Title + ".mp3");

            Console.WriteLine("Téléchargement en cours...");

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(video.GetUri(() => new DelegatingClient()), filePath);
            }

            Console.WriteLine("Téléchargement terminé : " + filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
        }
    }
}
