using UnityEngine;

public class CompanyManager : MonoBehaviour
{
    public void BuildFactory()
    {
        GameManager.Instance.profit += 500f;
        GameManager.Instance.emissions += 50f;
    }

    public void BuildRenewable()
    {
        GameManager.Instance.profit += 200f;
        GameManager.Instance.emissions += 5f;
    }

    public void BuyCarbonCredits()
    {
        if (GameManager.Instance.profit >= 300f)
        {
            GameManager.Instance.profit -= 300f;
            GameManager.Instance.emissions -= 20f;
        }
    }
}
