
namespace MusicPlayer
{
    //TODO : test me
    public class Playlist
    {
        List<string> queue;
        string filepath;

        public Playlist(List<string> queue)
        {
            this.queue = queue;
            this.filepath = string.Empty;
        }

        public void Add(string item) //On ne veut ajouter que le nom de la musique
        {
            this.queue.Add(item);
        }

        public void Remove(string item)
        {
            this.queue.Remove(item);
        }

        public string GetPlaylist() 
        {
            string res = "";
            using (StreamReader sr = new StreamReader(this.filepath)) 
            {
                
                while(sr.ReadLine() != null) 
                {
                    res += sr.ReadLine();
                }
            }
            return res;
        }

        public void Save()
        {
            string current = GetPlaylist();
            using (StreamWriter wr = new StreamWriter(filepath))
            {
                foreach(var t in this.queue) 
                {
                    if(!current.Contains(t))
                        wr.WriteLine(t);
                }
            }
        }
    }
}
