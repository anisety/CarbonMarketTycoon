using UnityEngine;

[CreateAssetMenu(fileName = "New Market Event", menuName = "Events/Market Event")]
public class MarketEvent : ScriptableObject
{
    public string eventName;
    [TextArea]
    public string description;
    public float carbonPriceModifier = 1.2f; // e.g., +20%
}
