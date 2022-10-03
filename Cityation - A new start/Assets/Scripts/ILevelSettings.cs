using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelSettings
{
    public List<Sprite> LevelObjectivePreviews { get; }
    public Sprite Preview { get; }
    public bool IsUnlocked { get; set; }
    public bool IsCompleted { get; set;  }
}

public class LevelSettings : ILevelSettings
{
    public LevelSettings(List<Sprite> levelObjectivePreviews, Sprite preview)
    {
        this.levelObjectivePreviews = levelObjectivePreviews;
        this.preview = preview;
    }

    public LevelSettings() : this(null, null)
    {
    }

    private List<Sprite> levelObjectivePreviews;
    private readonly Sprite preview;
    public List<Sprite> LevelObjectivePreviews => this.levelObjectivePreviews;
    public Sprite Preview => this.preview;
    public bool IsUnlocked { get; set; } = true;
    public bool IsCompleted { get; set; } = false;

}
