using UnityEngine;

public abstract class GameAsset : ScriptableObject
{
    public string assetName;
    public float cost;
    public float profitPerTurn;
    public float emissionsPerTurn;
}
