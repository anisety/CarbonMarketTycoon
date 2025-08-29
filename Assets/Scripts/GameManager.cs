using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float profit = 1000f;
    public float emissions = 0f;
    public float globalTemperature = 1.0f; // baseline = 1.0 (no warming)
    
    public float temperatureThreshold = 2.0f; // disaster trigger

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        // Every second = 1 year in game
        if (Time.frameCount % 60 == 0) 
        {
            UpdateTemperature();
            CheckForDisaster();
        }
    }

    void UpdateTemperature()
    {
        globalTemperature += emissions * 0.0001f;
    }

    void CheckForDisaster()
    {
        if (globalTemperature >= temperatureThreshold)
        {
            Debug.Log("⚠️ Disaster triggered! Your company is affected!");
            profit *= 0.8f; // lose 20% profit
        }
    }
}
