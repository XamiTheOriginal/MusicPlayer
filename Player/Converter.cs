﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
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

        public void ToMp3()
        {
            //TODO : A implem
        }
    }
}
