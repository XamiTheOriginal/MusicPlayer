using System;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;

namespace MusicPlayer.Downloader;

public class DownLoader
{
    public async Task DownloadAudioAsync(string videoUrl, string outputFilePath)
    {
        var youtube = new YoutubeClient();

        // Récupère le manifeste des flux disponibles
        var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

        // Sélectionne le flux audio avec le meilleur bitrate
        var audioStreamInfo = streamManifest
            .GetAudioStreams()
            .OrderByDescending(s => s.Bitrate)
            .FirstOrDefault();

        if (audioStreamInfo != null)
        {
            // Télécharge le flux audio
            await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, outputFilePath);
            Console.WriteLine($"✅ Audio téléchargé : {outputFilePath}");
        }
        else
        {
            Console.WriteLine("⚠️ Aucun flux audio trouvé.");
        }
    }
}