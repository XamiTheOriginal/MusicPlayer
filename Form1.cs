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
            player = new Player(@"C:\Users\maxim\OneDrive\Bureau\Music");
            downloader = new Downloader("oui",player.filepath);
            initialPath = player.getFilepath();
            listBoxSongs.SelectedIndex = -1;
            Console.WriteLine(downloader.filepath);
            LoadSongs();
        }

        private void LoadSongs()
        {
            string musicDirectory = player.filepath;
            if (!Directory.Exists(musicDirectory))
            {
                MessageBox.Show("Le répertoire n'existe pas : " + musicDirectory, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void timer1_Tick(object sender, EventArgs e)
        {

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
