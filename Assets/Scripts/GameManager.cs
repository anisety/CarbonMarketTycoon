using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public GameState currentState;
    public int currentYear = 1;
    public float timeScale = 1.0f; // Years per second

    [Header("Core Metrics")]
    public float profit = 1000f;
    public float emissions = 0f;
    public float carbonCredits = 100f;
    public float globalTemperature = 1.0f;

    [Header("Balancing")]
    public float temperatureSensitivity = 0.0001f;
    public float baseTemperatureIncrease = 0.02f;

    public event Action<int> OnYearChanged;
    public event Action<GameState> OnGameStateChanged;

    private float _timer;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.Playing);
    }

    private void Update()
    {
        if (currentState != GameState.Playing) return;

        _timer += Time.deltaTime * timeScale;
        if (_timer >= 1.0f)
        {
            _timer = 0;
            AdvanceYear();
        }
    }

    public void SetState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
        
        Time.timeScale = currentState == GameState.Playing ? 1 : 0;
    }

    private void AdvanceYear()
    {
        currentYear++;
        UpdateTemperature();
        
        OnYearChanged?.Invoke(currentYear);
        
        CompanyManager.Instance.UpdateCompanyMetrics();
        DisasterManager.Instance.CheckForDisasters(globalTemperature);
        MarketManager.Instance.UpdateMarket();
    }

    private void UpdateTemperature()
    {
        float tempIncrease = baseTemperatureIncrease + (emissions * temperatureSensitivity);
        globalTemperature += tempIncrease;
    }

    public void AdjustMetrics(float profitChange, float emissionsChange, float creditsChange = 0)
    {
        profit += profitChange;
        emissions += emissionsChange;
        carbonCredits += creditsChange;
    }
}
