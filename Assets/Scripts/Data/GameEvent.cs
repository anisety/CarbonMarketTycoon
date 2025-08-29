// Assets/Scripts/Data/GameEvent.cs
using UnityEngine;

public abstract class GameEvent : ScriptableObject
{
    public string eventName;
    [TextArea]
    public string description;
    public Sprite eventIcon;
}
