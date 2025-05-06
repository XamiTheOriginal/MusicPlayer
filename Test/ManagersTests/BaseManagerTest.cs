using System.Collections.Generic;
using Xunit;
using MusicPlayer.SongsHandler.Managers;
using System.IO;
using MusicPlayer.SongsHandler;

namespace Test;

#region Emulation

public class DummyItem : IIdentifiable
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class DummyManager : BaseManager<DummyItem>
{
    public DummyManager(string filePath) : base(filePath) { }

    protected override void InitializeDefaultData()
    {
        AddItem(new DummyItem { Title = "Default Item" });
    }
}

#endregion

public class BaseManagerTest
{
    private string tempFilePath => Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");

    [Fact]
    public void AddItem_ShouldAssignUniqueId_AndSaveToList()
    {
        // Arrange
        string filePath = tempFilePath;
        var manager = new DummyManager(filePath);
        var item = new DummyItem { Title = "Test Item" };

        // Act
        manager.AddItem(item);
        var items = manager.GetAllItems();

        // Assert
        Assert.Single(items);
        Assert.Equal("Test Item", items[0].Title);
        Assert.True(items[0].Id > 0);
    }

    [Fact]
    public void RemoveItem_ShouldRemoveFromList_AndRecycleId()
    {
        string filePath = tempFilePath;
        var manager = new DummyManager(filePath);
        var item = new DummyItem { Title = "ToRemove" };

        manager.AddItem(item);
        manager.RemoveItem(item);

        Assert.Empty(manager.GetAllItems());

        // Re-add an item to check ID recycling
        var item2 = new DummyItem { Title = "ReAdded" };
        manager.AddItem(item2);

        Assert.Equal(item.Id, item2.Id); // L'ID a été recyclé
    }

    [Fact]
    public void LoadState_ShouldRestoreSavedItems()
    {
        string filePath = tempFilePath;

        // Phase 1 : Création et ajout
        var manager1 = new DummyManager(filePath);
        manager1.AddItem(new DummyItem { Title = "PersistentItem" });

        // Phase 2 : Nouveau manager, même fichier
        var manager2 = new DummyManager(filePath);
        manager2.LoadState();

        var items = manager2.GetAllItems();
        Assert.Single(items);
        Assert.Equal("PersistentItem", items[0].Title);
    }
    [Fact]
    public void GetItemByTitle_ShouldReturnCorrectItem()
    {
        var manager = new DummyManager(tempFilePath);
        var item = new DummyItem { Title = "FindMe" };
        manager.AddItem(item);

        var found = manager.GetItemByTitle("FindMe");

        Assert.NotNull(found);
        Assert.Equal(item.Title, found.Title);
    }
    [Fact]
    public void GetItemById_ShouldThrowIfNotFound()
    {
        var manager = new DummyManager(tempFilePath);

        Assert.Throws<KeyNotFoundException>(() => manager.GetItemById(999));
    }

    
    

}