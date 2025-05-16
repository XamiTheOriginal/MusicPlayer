using TagLib;
using System.IO;
using Avalonia.Media.Imaging;

namespace MusicPlayer
{
    public static class AlbumArtHelper
    {
        public static Bitmap? GetAlbumArt(string filePath)
        {
            var file = TagLib.File.Create(filePath);
            if (file.Tag.Pictures.Length > 0)
            {
                var picture = file.Tag.Pictures[0];
                using var ms = new MemoryStream(picture.Data.Data);
                return new Bitmap(ms);
            }

            return null;
        }
    }
}