using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Metric Displays")]
    public TextMeshProUGUI profitText;
    public TextMeshProUGUI emissionsText;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI carbonCreditsText;
    public TextMeshProUGUI carbonPriceText;

    [Header("Notification Panel")]
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationTitle;
    public TextMeshProUGUI notificationMessage;

    [Header("Asset Buttons")]
    public Transform assetButtonContainer;
    public GameObject assetButtonPrefab;
    public List<GameAsset> availableAssets;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnYearChanged += UpdateUI;
        UpdateUI(GameManager.Instance.currentYear);
        PopulateAssetButtons();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnYearChanged -= UpdateUI;
        }
    }

    private void UpdateUI(int year)
    {
        profitText.text = $"Profit: ${GameManager.Instance.profit:F0}";
        emissionsText.text = $"Emissions: {GameManager.Instance.emissions:F2} tCO₂";
        temperatureText.text = $"Temp: {GameManager.Instance.globalTemperature:F2}°C";
        yearText.text = $"Year: {year}";
        carbonCreditsText.text = $"Credits: {GameManager.Instance.carbonCredits:F0}";
        carbonPriceText.text = $"Carbon Price: ${MarketManager.Instance.carbonPrice:F2}";
    }

    public void ShowNotification(string title, string message, float duration)
    {
        notificationTitle.text = title;
        notificationMessage.text = message;
        StartCoroutine(ShowNotificationCoroutine(duration));
    }

    private IEnumerator ShowNotificationCoroutine(float duration)
    {
        notificationPanel.SetActive(true);
        yield return new WaitForSeconds(duration);
        notificationPanel.SetActive(false);
    }

    private void PopulateAssetButtons()
    {
        foreach (Transform child in assetButtonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var asset in availableAssets)
        {
            var buttonGO = Instantiate(assetButtonPrefab, assetButtonContainer);
            var buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            var button = buttonGO.GetComponent<Button>();

            buttonText.text = $"Buy {asset.assetName}\n(${asset.cost})";
            button.onClick.AddListener(() => CompanyManager.Instance.BuyAsset(asset));
        }
    }
}
