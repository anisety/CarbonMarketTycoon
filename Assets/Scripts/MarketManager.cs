using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    [Header("Market State")]
    public float carbonPrice = 50f; // Price per ton of CO2
    public float priceVolatility = 0.1f;
    public float marketTrend = 0.01f; // Slight upward trend

    [Header("Events")]
    public List<MarketEvent> marketEvents;
    public float eventChance = 0.1f; // 10% chance per year

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateMarket()
    {
        // Fluctuate price
        float randomFactor = Random.Range(-priceVolatility, priceVolatility);
        carbonPrice *= (1 + randomFactor + marketTrend);
        carbonPrice = Mathf.Max(10f, carbonPrice); // Floor price

        // Trigger random event
        if (Random.value < eventChance)
        {
            TriggerRandomEvent();
        }
    }

    public bool BuyCarbonCredits(float amountToBuy)
    {
        float cost = amountToBuy * carbonPrice;
        if (GameManager.Instance.profit >= cost)
        {
            GameManager.Instance.AdjustMetrics(-cost, 0, amountToBuy);
            Debug.Log($"Bought {amountToBuy} credits for ${cost}");
            return true;
        }
        return false;
    }

    public void SellCarbonCredits(float amountToSell)
    {
        if (GameManager.Instance.carbonCredits >= amountToSell)
        {
            float revenue = amountToSell * carbonPrice;
            GameManager.Instance.AdjustMetrics(revenue, 0, -amountToSell);
            Debug.Log($"Sold {amountToSell} credits for ${revenue}");
        }
    }

    private void TriggerRandomEvent()
    {
        if (marketEvents.Count == 0) return;

        MarketEvent randomEvent = marketEvents[Random.Range(0, marketEvents.Count)];
        carbonPrice *= randomEvent.carbonPriceModifier;
        
        UIController.Instance.ShowNotification(randomEvent.eventName, randomEvent.description, 5f);
        Debug.Log($"Market Event: {randomEvent.eventName}");
    }
}
