
namespace MusicPlayer
{
    //TODO : test me
    public class Playlist
    {
        public List<int> Queue;
        public string Name;
        public Playlist(List<int> queue, string name)
        {
            Queue = queue;
            Name = name;
        }

        public void Add(int item) //On ne veut ajouter que le nom de la musique
        {
            Queue.Add(item);
        }

        public void Remove(int item)
        {
            Queue.Remove(item);
        }
        
        
        
    }
}
