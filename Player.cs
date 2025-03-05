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
        private Queue<int> _songIdQueue = new Queue<int>();
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
            CurrentSongId = 0;
        }

        public string GetFilePath()
        {
            return CurrentSong.Filepath;
        }
        
        public void PlayDaMusic()
        {
            try
            {
                if (_audioFile != null) _audioFile.Dispose();
                if (_outputDevice != null) _outputDevice.Dispose();

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
            _songIdQueue = new Queue<int>(playlist.GetSongList());
            if (_songIdQueue.Count > 0)
            {
                CurrentSongId = _songIdQueue.Dequeue();
                PlayDaMusic();
            }
            else
            {
                Console.WriteLine("La playlist est vide.");
            }

        }
    }
    
}
