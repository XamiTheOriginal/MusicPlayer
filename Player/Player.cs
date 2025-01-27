using NAudio.Wave;

namespace MusicPlayer
{
    public class Player
    {
        public string filepath;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        private bool isPlaying = false;
        private bool isPaused = false;
        public Player(string path)
        {
            this.filepath = path;
        }

        public string getFilepath()
        { 
            return this.filepath; 
        }

        public void setFilepath(string path)
        {
            this.filepath = path;
        }
        public void playDaMusic()
        {
            if (this.filepath == null)
            {
                throw new ArgumentException(this.getFileName());
            }
            if (isPaused)
            {
                outputDevice.Stop();
                audioFile = new AudioFileReader(this.filepath);
                outputDevice.Init(audioFile);
                outputDevice.Play();
                isPlaying = true;
                isPaused = false;
                return;
            }
            else 
            {
                Task.Run(() =>
                {
                    try
                    {
                        using (audioFile = new AudioFileReader(this.filepath))
                        using (outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFile);
                            outputDevice.PlaybackStopped += (sender, args) =>
                            {
                                isPlaying = false;
                            };  
                            outputDevice.Play();
                            isPlaying = true;
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
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

        public void pauseDaMusic()
        {
            if (isPlaying && !isPaused)
            {
                outputDevice.Pause();
                isPlaying = false;
                isPaused = true;
            }
        }

        public string getFileName()
        /*
         * Retourne uniquement le nom du fichier,
         * en prenant en argument le path entier
         */
        {
            if (string.IsNullOrEmpty(this.filepath))
                throw new ArgumentException("filepath is null or empty");
            string[] filename = this.filepath.Split('\\');
            string[] res = filename[filename.Length - 1].Split('.');
            return res[0];
        }

        public void playDaPlaylist()
        {
            if (!Directory.Exists(this.filepath))
            {
                throw new ArgumentException($"{nameof(this.filepath)} does not exist");
            }

            string path = this.filepath;
            foreach (string file in Directory.GetFiles(this.filepath, "*.mp3"))
            {
                try
                {
                    this.filepath = file;
                    Console.WriteLine($"Now playing: {this.getFileName()}");
                    this.playDaMusic();
                    this.filepath = path;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error while playing {file}: {e.Message}");
                }
            }
        }
    }
}
