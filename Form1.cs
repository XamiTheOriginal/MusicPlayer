using Microsoft.VisualBasic.ApplicationServices;
using System.IO;
using System.Linq;


namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        private bool _isPlaying = false;


        private Player _player;
        private string _initialPath;
        private Downloader _downloader;
        public Form1()
        {
            InitializeComponent();
            string musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Player player = new Player(musicPath); //�a s'adapte au nom d'utilisateur et �a recherche les fichiers dans
            //le dossier musique par d�faut de Windows
            
            if (player == null)
            {
                throw new Exception("Player object is not initialized.");
            }

            if (string.IsNullOrEmpty(player.GetFilepath()))
            {
                throw new Exception("Player filepath is null or empty.");
            }
            
            Console.WriteLine($"musicPath: {musicPath}");
            Console.WriteLine($"player: {player != null}");
            Console.WriteLine($"player.filepath: {player?.GetFilepath()}");
            Console.WriteLine($"downloader: {_downloader != null}");
            
            //downloader = new Downloader("oui",player.filepath);
            _initialPath = player.GetFilepath();
            listBoxSongs.SelectedIndex = -1;
            //Console.WriteLine(downloader.filepath);
            LoadSongs();
        }

        private void LoadSongs()
        {
            string musicDirectory = _player.GetFilepath();
            if (!Directory.Exists(musicDirectory))
            {
                MessageBox.Show("This directory doesn't exist : " + musicDirectory, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] files = Directory.GetFiles(musicDirectory);
            foreach (string file in files) 
            {
                listBoxSongs.Items.Add(file);
                Console.WriteLine(file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedItem == null)
            {
                MessageBox.Show("Veuillez s�lectionner une chanson.");
                return;
            }
            if (!_isPlaying)
            {
                _player.PlayDaMusic();
                buttonPlayPause.Text = "Pause";
                _isPlaying = true;
            }
            else
            {
                _player.PauseDaMusic();
                buttonPlayPause.Text = "Play";
                _isPlaying = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (listBoxSongs.SelectedItem != null)
                {
                    _player.PauseDaMusic();
                    string selectedSong = listBoxSongs.SelectedItem.ToString();
                    _player.SetFilepath(selectedSong);
                    TitleLab.Text = _player.GetFileName();
                }
        }
    }
}
