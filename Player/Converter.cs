using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer //PUTE
{
    public class Converter
    {
        private string _filepath;
        public Converter( string filepath)
        {
            _filepath = filepath;
        }

        public void ToWave(string outfile)
        {
            using(var reader = new MediaFoundationReader(_filepath))
            {
                 WaveFileWriter.CreateWaveFile(outfile, reader);
            }
        }
        
    }
}
