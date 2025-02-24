using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
