using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MusicPlayer;

public class Profiler
{
  public string Name;
  private Moods? MostPlayed {get; set;}
  private Dictionary<Moods, int> _moodsData; //integer represents amount of time, might have to create a class to represent it
  private string _path = "../DATA/userdata.json";

  private Profiler()
  {
    Profiler? item = JsonSerializer.Deserialize<Profiler>(_path);
    if (item != null)
    {
      Name = item.Name;
      MostPlayed = item.MostPlayed;
      _moodsData = item._moodsData;
    }
    else
    {
      Name = "Default";
      MostPlayed = null;
      _moodsData = new Dictionary<Moods, int>();
      File.Create(_path);
    }
  }

  public void SaveData()
  {
    string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
    File.WriteAllText(_path, json);
  }

  public void ChangePath(string newPath)
  {
    File.Move(_path, newPath);
    _path = newPath;
  }

  public void UpdateData(Moods mood, int time)
  {
    _moodsData[mood] += time;
    _updateMostPlayed();
  }

  private void _updateMostPlayed()
  {
    Moods? maxPlayed = MostPlayed;
    int maxTime = 0;
    if(MostPlayed != null)
      maxTime = _moodsData[(Moods)MostPlayed!];
    foreach (var keyValuePair in _moodsData)
    {
      if (keyValuePair.Value > maxTime)
      {
        maxPlayed = keyValuePair.Key;
        maxTime = keyValuePair.Value;
      }
    }
    MostPlayed = maxPlayed;
  }
  
}