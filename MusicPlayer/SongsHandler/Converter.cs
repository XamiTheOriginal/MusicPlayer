using NAudio.Wave;
using System.IO;

namespace MusicPlayer.SongsHandler
{
    public static class Converter
    {
        static Converter() { }
        
        public static string ToWave(string infile)
        {
            string outfile = infile;
            if (Path.GetExtension(infile) != ".wav")
            {
                outfile = Path.ChangeExtension(infile, ".wav");
                using (var reader = new MediaFoundationReader(infile))
                {
                    WaveFileWriter.CreateWaveFile(outfile, reader);
                }
            }

            return outfile;
        }
    }
}
