using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanyManager : MonoBehaviour
{
    public static CompanyManager Instance;

    public List<GameAsset> ownedAssets = new List<GameAsset>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateCompanyMetrics()
    {
        float yearlyProfit = 0;
        float yearlyEmissions = 0;

        foreach (var asset in ownedAssets)
        {
            yearlyProfit += asset.profitPerTurn;
            yearlyEmissions += asset.emissionsPerTurn;
        }

        GameManager.Instance.AdjustMetrics(yearlyProfit, yearlyEmissions);
    }

    public bool BuyAsset(GameAsset asset)
    {
        if (GameManager.Instance.profit >= asset.cost)
        {
            GameManager.Instance.AdjustMetrics(-asset.cost, 0);
            ownedAssets.Add(asset);
            Debug.Log($"Purchased {asset.assetName} for ${asset.cost}");
            return true;
        }
        Debug.LogWarning($"Not enough profit to buy {asset.assetName}");
        return false;
    }

    public void SellAsset(GameAsset asset)
    {
        if (ownedAssets.Remove(asset))
        {
            float salePrice = asset.cost * 0.75f; // Sell for 75% of original cost
            GameManager.Instance.AdjustMetrics(salePrice, 0);
            Debug.Log($"Sold {asset.assetName} for ${salePrice}");
        }
    }
}
