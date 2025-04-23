using System.IO;
using System.Threading.Tasks;
using MusicPlayer.Downloader;
using Xunit;

namespace MusicPlayer.Tests
{
    public class YoutubeAudioDownloaderTests
    {
        [Fact]
        public async Task DownloadAudioAsync_ValidUrl_DownloadsAudioFile()
        {
            // Arrange
            var downloader = new DownLoader();
            var videoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"; // une vidéo publique connue
            var outputFile = Path.Combine(Path.GetTempPath(), "test_audio.webm");

            // Supprimer le fichier s’il existe déjà
            if (File.Exists(outputFile))
                File.Delete(outputFile);

            // Act
            await downloader.DownloadAudioAsync(videoUrl, outputFile);

            // Assert
            Assert.True(File.Exists(outputFile), "Le fichier audio n’a pas été téléchargé.");

            // Nettoyage
            File.Delete(outputFile);
        }
    }
}