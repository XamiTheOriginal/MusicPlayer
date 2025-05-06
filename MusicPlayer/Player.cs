using System;
 using System.Collections.Generic;
 using NAudio.Wave;
 using Microsoft.Extensions.DependencyInjection;
 using MusicPlayer.SongsHandler;
 using MusicPlayer.SongsHandler.Managers;

 
 namespace MusicPlayer
 {
     public class Player
     {
         #region ClassVariables
 
         public int CurrentSongId;
         private Queue<int> _nextSongIdQueue = new Queue<int>();
         private Queue<int> _previousSongQueue = new Queue<int>();
 
         private WaveOutEvent _outputDevice;
         private AudioFileReader _audioFile;

         private bool _isPlaying;
         private Song CurrentSong
         {
             get
             {
                 var songsManager = ServiceLocator.Instance.GetRequiredService<SongsManager>();
                 return songsManager.GetItemById(CurrentSongId);
             }
         }
 
         #endregion
 
         // Constructeur sans injection de AudioFileReader
         public Player(WaveOutEvent outputDevice)
         {
             _outputDevice = outputDevice;
             CurrentSongId = 1;
         }
 
         // Méthode pour changer la chanson actuelle
         public void SetCurrentSongId(int songId)
         {
             CurrentSongId = songId;
             PlayDaMusic(); // Lance la chanson immédiatement après la mise à jour de l'ID
         }
 
         public string GetFilePath()
         {
             return CurrentSong.Filepath;
         }
 
         public void PlayDaMusic()
         {
             try
             {
                 // Si déjà en train de jouer, on ne relance pas
                 if (_isPlaying)
                 {
                     PauseDaMusic();
                     return;
                 }
 
                 // Créer un nouveau lecteur de fichier audio avec le chemin du fichier actuel
                 _audioFile?.Dispose(); // Libération de l'ancien fichier si nécessaire
                 _audioFile = new AudioFileReader(CurrentSong.Filepath);
 
                 // Initialisation du périphérique de sortie audio
                 _outputDevice?.Dispose();
                 _outputDevice = new WaveOutEvent();
                 _isPlaying = true;
 
                 // Gestion de fin automatique
                 _outputDevice.PlaybackStopped += (s, e) =>
                 {
                     // Vérifie que la musique est bien terminée (et pas juste "pause")
                     if (_audioFile.Position >= _audioFile.Length)
                     {
                         NextSong();
                     }

                     NextSong();
                 };
 
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
             if (_outputDevice != null && _isPlaying)

             {
                 _outputDevice.Pause();

                 _isPlaying = false;
             }
         }
 
         public void PlayDaPlaylist(int id)
         {
             var playlistsManager = ServiceLocator.Instance.GetRequiredService<PlaylistsManager>();
             Playlist playlist = playlistsManager.GetItemById(id);
             _nextSongIdQueue = new Queue<int>(playlist.SongList);
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