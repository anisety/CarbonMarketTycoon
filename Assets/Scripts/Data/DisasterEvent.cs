using UnityEngine;

[CreateAssetMenu(fileName = "New Disaster Event", menuName = "Events/Disaster Event")]
public class DisasterEvent : ScriptableObject
{
    public string eventName;
    [TextArea]
    public string description;
    public float profitImpact = -0.2f; // e.g., -20%
    public float emissionsImpact = 0f;
    public float temperatureThreshold = 2.0f;
}
