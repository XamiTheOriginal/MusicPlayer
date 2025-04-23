using MusicPlayer.SongsHandler;
using Xunit;
using System.Collections.Generic;

namespace MusicPlayer.Tests
{
    public class PlaylistTests
    {
        // Test pour vérifier que le constructeur initialise correctement le titre et la liste des chansons
        [Fact]
        public void Constructor_SetsTitleAndSongsCorrectly()
        {
            var playlist = new Playlist("Test", new List<int> { 1, 2 });

            Assert.Equal("Test", playlist.Title);
            Assert.Equal(2, playlist.SongCount);
            Assert.False(playlist.IsEmpty);
        }

        // Test pour vérifier l'ajout d'une chanson à la playlist
        [Fact]
        public void AddSong_IncreasesCount()
        {
            var playlist = new Playlist("Test", new List<int>());
            playlist.AddSong(3);

            Assert.Single(playlist.SongList);  // Vérifie que la playlist contient une chanson
            Assert.Equal(1, playlist.SongCount);  // Vérifie que le compteur est à 1
        }

        // Test pour vérifier la suppression d'une chanson de la playlist
        [Fact]
        public void RemoveSong_DecreasesCount()
        {
            var playlist = new Playlist("Test", new List<int> { 1, 2 });
            playlist.RemoveSong(1);

            Assert.Single(playlist.SongList);  // Vérifie que la playlist contient une chanson
            Assert.Equal(1, playlist.SongCount);  // Vérifie que le compteur est à 1
        }

        // Test pour vérifier que les titres des chansons sont correctement retournés
        [Fact]
        public void GetSongTitles_ReturnsCorrectStrings()
        {
            var playlist = new Playlist("Test", new List<int> { 1, 2 });

            var titles = playlist.GetSongTitles();

            Assert.Contains("1", titles);  // Vérifie que "1" est présent dans la liste
            Assert.Contains("2", titles);  // Vérifie que "2" est présent dans la liste
        }

        // Test pour vérifier qu'une playlist vide est correctement identifiée
        [Fact]
        public void Playlist_ShouldBeEmptyWhenNoSongs()
        {
            var playlist = new Playlist("Empty Playlist", new List<int>());

            Assert.True(playlist.IsEmpty);  // Vérifie que la playlist est vide
        }

        // Test pour vérifier qu'une playlist avec des chansons n'est pas vide
        [Fact]
        public void Playlist_ShouldNotBeEmptyWhenHasSongs()
        {
            var playlist = new Playlist("Non-empty Playlist", new List<int> { 1 });

            Assert.False(playlist.IsEmpty);  // Vérifie que la playlist n'est pas vide
        }

        // Test pour vérifier l'override ToString()
        [Fact]
        public void ToString_ShouldReturnTitle()
        {
            var playlist = new Playlist("Test", new List<int>());

            var result = playlist.ToString();

            Assert.Equal("Test", result);  // Vérifie que ToString() retourne le titre
        }
    }
}
