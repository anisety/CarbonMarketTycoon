using System.Collections.Generic;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public static DisasterManager Instance;

    public List<DisasterEvent> disasterEvents;
    public float baseDisasterChance = 0.05f; // 5% base chance per year

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CheckForDisasters(float currentTemp)
    {
        foreach (var disaster in disasterEvents)
        {
            if (currentTemp >= disaster.temperatureThreshold)
            {
                float chance = baseDisasterChance + (currentTemp - disaster.temperatureThreshold); // Chance increases with temp
                if (Random.value < chance)
                {
                    TriggerDisaster(disaster);
                }
            }
        }
    }

    private void TriggerDisaster(DisasterEvent disaster)
    {
        Debug.LogWarning($"DISASTER: {disaster.eventName}!");
        GameManager.Instance.AdjustMetrics(GameManager.Instance.profit * disaster.profitImpact, disaster.emissionsImpact);
        
        UIController.Instance.ShowNotification(disaster.eventName, disaster.description, 8f);
    }
}
