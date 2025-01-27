using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Converter
    {
        WaveFileWriter writer;
        string filepath;

        public Converter(WaveFileWriter writer, string filepath)
        {
            this.writer = writer;
            this.filepath = filepath;
        }

        public void toWave()
        {
            //TODO : A implem
        }

        public void toMP3()
        {
            //TODO : A implem
        }
    }
}
