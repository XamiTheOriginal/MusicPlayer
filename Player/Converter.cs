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
        WaveFileWriter _writer;
        string _filepath;

        public Converter(WaveFileWriter writer, string filepath)
        {
            this._writer = writer;
            this._filepath = filepath;
        }

        public void ToWave()
        {
            //TODO : A implem
        }

        public void ToMp3()
        {
            //TODO : A implem
        }
    }
}
