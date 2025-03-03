using MusicPlayer.SongsHandler;
using NAudio.Wave;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.SongsHandler.Managers;


namespace MusicPlayer
{
    
    //faire un signleton avec le player comme les managers
    public class Player
    {
        public int CurrentSongId;
        private Queue<int> _songIdQueue = new Queue<int>();
        private Song? CurrentSong
        {
            get
            {
                if (CurrentSongId == -1)
                {
                    Console.WriteLine("Aucune chanson sélectionnée.");
                    return null;
                }

                var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
                try
                {
                    return songsManager.GetItemById(CurrentSongId);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Erreur : Impossible de trouver la chanson avec l'ID {CurrentSongId}, sélection automatique de la première.");
                    CurrentSongId = songsManager.GetAllItems().FirstOrDefault()?.Id ?? -1;
                    return songsManager.GetAllItems().FirstOrDefault();
                }
            }
        }


        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        private bool _isPlaying = false;
        private bool _isPaused = false;
        
        public Player()
        {
            CurrentSongId = -1;
        }
        
        public void PlayDaMusic()
        {
            var songsManager =  ServiceLocator.Instance.GetRequiredService<SongsManager>();

            if (_isPaused)
            {
                _outputDevice.Stop();
                _audioFile = new AudioFileReader(CurrentSong.Filepath);
                _outputDevice.Init(_audioFile);
                _outputDevice.Play();
                _isPlaying = true;
                _isPaused = false;
                return;
            }
            else 
            {
                Task.Run(() =>
                {
                    try
                    {
                        using (_audioFile = new AudioFileReader(CurrentSong.Filepath))
                        using (_outputDevice = new WaveOutEvent())
                        {
                            _outputDevice.Init(_audioFile);
                            _outputDevice.PlaybackStopped += (sender, args) =>
                            {
                                _isPlaying = false;
                            };  
                            _outputDevice.Play();
                            _isPlaying = true;
                            while (_outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                });
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
            int temp = _songIdQueue.Dequeue();
            CurrentSongId = temp;
            PlayDaMusic();
        }
    }
    
}
