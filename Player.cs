using MusicPlayer.SongsHandler;
using NAudio.Wave;


namespace MusicPlayer
{
    
    
    public class Player
    {
        
        public string Filepath;

        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;

        private bool _isPlaying = false;
        private bool _isPaused = false;
        public Player(string path)
        {
            this.Filepath = path;
        }

        public string GetFilepath()
        { 
            return this.Filepath; 
        }

        public void SetFilepath(string path)
        {
            this.Filepath = path;
        }
        public void PlayDaMusic()
        {
            if (this.Filepath == null)
            {
                throw new ArgumentException(this.GetFileName());
            }
            if (_isPaused)
            {
                _outputDevice.Stop();
                _audioFile = new AudioFileReader(this.Filepath);
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
                        using (_audioFile = new AudioFileReader(this.Filepath))
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

        public string GetFileName()
        /*
         * Retourne uniquement le nom du fichier,
         * en prenant en argument le path entier
         */
        {
            if (string.IsNullOrEmpty(this.Filepath))
                throw new ArgumentException("filepath is null or empty");
            string[] filename = this.Filepath.Split('\\');
            string[] res = filename[filename.Length - 1].Split('.');
            return res[0];
        }

        public void PlayDaPlaylist()
        {
            if (!Directory.Exists(this.Filepath))
            {
                throw new ArgumentException($"{nameof(this.Filepath)} does not exist");
            }

            string path = this.Filepath;
            foreach (string file in Directory.GetFiles(this.Filepath, "*.mp3"))
            {
                try
                {
                    this.Filepath = file;
                    Console.WriteLine($"Now playing: {this.GetFileName()}");
                    this.PlayDaMusic();
                    this.Filepath = path;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error while playing {file}: {e.Message}");
                }
            }
        }
    }
}
