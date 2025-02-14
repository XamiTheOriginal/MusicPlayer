using Microsoft.VisualBasic.ApplicationServices;
using System.IO;
using System.Linq;


namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        private bool isPlaying = false;


        private Player player;
        private string initialPath;
        private Downloader downloader;
        public Form1()
        {
            InitializeComponent();
            string musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Player player = new Player(musicPath); //ça s'adapte au nom d'utilisateur et ça recherche les fichiers dans
            //le dossier musique par défaut de Windows
            
            if (player == null)
            {
                throw new Exception("Player object is not initialized.");
            }

            if (string.IsNullOrEmpty(player.getFilepath()))
            {
                throw new Exception("Player filepath is null or empty.");
            }
            
            Console.WriteLine($"musicPath: {musicPath}");
            Console.WriteLine($"player: {player != null}");
            Console.WriteLine($"player.filepath: {player?.getFilepath()}");
            Console.WriteLine($"downloader: {downloader != null}");
            
            //downloader = new Downloader("oui",player.filepath);
            initialPath = player.getFilepath();
            listBoxSongs.SelectedIndex = -1;
            //Console.WriteLine(downloader.filepath);
            LoadSongs();
        }

        private void LoadSongs()
        {
            string musicDirectory = player.getFilepath();
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
                MessageBox.Show("Veuillez sélectionner une chanson.");
                return;
            }
            if (!isPlaying)
            {
                player.playDaMusic();
                buttonPlayPause.Text = "Pause";
                isPlaying = true;
            }
            else
            {
                player.pauseDaMusic();
                buttonPlayPause.Text = "Play";
                isPlaying = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (listBoxSongs.SelectedItem != null)
                {
                    player.pauseDaMusic();
                    string selectedSong = listBoxSongs.SelectedItem.ToString();
                    player.setFilepath(selectedSong);
                    TitleLab.Text = player.getFileName();
                }
        }
    }
}
