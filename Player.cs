using MusicPlayer.SongsHandler;
using NAudio.Wave;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;


namespace MusicPlayer
{
    
    //faire un singleton avec le player comme les managers
    public class Player
    {
        public int CurrentSongId;
        private Queue<int> _nextSongIdQueue = new Queue<int>();
        private Queue<int> _previousSongQueue = new Queue<int>();
        private Song CurrentSong
        {
            get
            {
                var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
                return songsManager.GetItemById(CurrentSongId);
            }
        }


        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        private bool _isPlaying = false;
        private bool _isPaused = false;
        
        public Player()
        {
            CurrentSongId = 1;
        }

        public string GetFilePath()
        {
            return CurrentSong.Filepath;
        }
        
        public void PlayDaMusic()
        {
            try
            {
                if (_audioFile is not null) _audioFile.Dispose();
                if (_outputDevice is not null) _outputDevice.Dispose();

                _audioFile = new AudioFileReader(CurrentSong.Filepath);
                _outputDevice = new WaveOutEvent();
                _outputDevice.Init(_audioFile);
                _outputDevice.Play();
                _isPlaying = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la lecture : {e.Message}");
            }
        }


        public void PauseDaMusic()
        {
            if (_isPlaying && !_isPaused)
            {
                _outputDevice.Pause();
                _isPlaying = false;
                _isPaused = true;
            }
        }
            
        public void PlayDaPlaylist(int id)
        {
            var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
            var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();
            Playlist playlist = playlistsManager.GetItemById(id);
            _nextSongIdQueue = new Queue<int>(playlist.GetSongList());
            _previousSongQueue = new Queue<int>();
            if (_nextSongIdQueue.Count > 0)
            {
                NextSong();
            }
            else
            {
                Console.WriteLine("La playlist est vide.");
            }

        }

        public void NextSong()
        {
            _previousSongQueue.Enqueue(CurrentSongId);
            CurrentSongId = _nextSongIdQueue.Dequeue();
            PlayDaMusic();
        }

        public void PreviousSong()
        {
            _nextSongIdQueue.Enqueue(CurrentSongId);
            CurrentSongId = _previousSongQueue.Dequeue();
            PlayDaMusic();
        }
    }
    
}
