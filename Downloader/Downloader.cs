using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Downloader
    {
        public string Url;
        public string Filepath;
        public Downloader(string url, string path) //We will always use the path if the music directory
                                                   //Url is meant to be a youtube video, we will parse it later to check it is a good one
        {
            this.Url = url;
            this.Filepath = path;
        }
        /* private bool IsMP3(byte[] buf)
         {
             if (buf[0] == 0xFF && (buf[1] & 0xF6) > 0xF0 && (buf[2] & 0xF0) != 0xF0)
             {
                 return true;
             }
             return false;
         }*/

        private string GetNameNoExtension()
        {
            string res = "";
            int i = 0;
            int l = this.Filepath.Length;
            while (i < l && this.Filepath[i] != '.') //meant to find the first . in the path which should be the extension
            {
                res += this.Filepath[i];
            }
            return res;
        }
        /* public void toMP3() //Will change all the Directory file to .mp3
         {
             foreach(string fileName in Directory.GetFiles(this.filepath))
             {
                 using (FileStream fs = new FileStream(fileName,FileMode.Open))
                 {
                     byte[] buffer = new byte[4];
                     fs.Read(buffer, 0, 4);
                     if (!IsMP3(buffer))
                     {
                         //buffer[0] = 0xFF;
                         //buffer[1] = 0xF6;
                         //buffer[2] = 0xF0;
                         string newName = this.getNameNoExtension() + ".mp3";
                         File.Move(fileName, newName);
                     }
                 }
             }*/
    }
}
