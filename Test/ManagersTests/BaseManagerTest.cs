using Xunit;
using MusicPlayer.SongsHandler.Managers;
using System.IO;
namespace Test;

#region Emulation

public class DummyItem
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
}