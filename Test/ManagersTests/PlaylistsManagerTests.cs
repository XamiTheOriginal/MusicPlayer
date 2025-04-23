using MusicPlayer.SongsHandler;
using MusicPlayer.SongsHandler.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MusicPlayer.Tests
{
    public class PlaylistsManagerTests
    {
        private string GetTempFilePath() =>
            Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");

        [Fact]
        public void LoadState_CreatesDefaultPlaylist_IfFileMissing()
        {
            var path = GetTempFilePath();
            var manager = new TestablePlaylistsManager(path);
            manager.LoadState();

            var items = manager.GetAllItems();

            Assert.Single(items);
            Assert.Equal("Default", items[0].Title);
        }

        [Fact]
        public void AddAndRetrievePlaylist_WorksCorrectly()
        {
            var path = GetTempFilePath();
            var manager = new TestablePlaylistsManager(path);
            manager.LoadState();

            var p = new Playlist("Rock", new List<int>());
            manager.AddItem(p);

            var retrieved = manager.GetItemByTitle("Rock");
            Assert.NotNull(retrieved);
            Assert.Equal("Rock", retrieved.Title);
        }

        private class TestablePlaylistsManager : PlaylistsManager
        {
            public TestablePlaylistsManager(string customPath)
                : base()
            {
                typeof(BaseManager<Playlist>)
                    .GetField("SaveFilePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .SetValue(this, customPath);
            }
        }
    }
}