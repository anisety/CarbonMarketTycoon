🌍💰 Carbon Market Tycoon

A Unity & C# simulation game about climate economics, sustainability, and corporate decision-making

🚀 Core Concept

In Carbon Market Tycoon, you run a growing company that must balance:

Profits 💵 → invest in factories, expand production, chase short-term gains.

Emissions 🌫️ → your industrial activities release CO₂.

Carbon Market ♻️ → buy/sell carbon credits, adopt renewable tech, or risk environmental collapse.

The challenge: polluting makes you rich quickly… but increases the Global Temperature Meter 🌡️. If the planet heats too much, disasters strike and your company suffers.

🎮 Gameplay Loop

Make Decisions Each Turn

Build factories (high profit, high emissions).

Invest in renewable energy (lower profit, lower emissions).

Trade carbon credits on the market.

Monitor Global Metrics

Profit (💵)

Emissions (🌫️)

Global Temperature Meter (🌡️)

Face Consequences

High temperatures trigger disasters (storms, floods, supply chain collapse).

Balanced approach keeps profits stable and saves the planet.

🛠️ Tech Stack

Unity (2021+ recommended)

C# Scripts for game logic

Unity UI Toolkit or Canvas UI for dashboards/menus

ScriptableObjects for data-driven events (e.g., random climate disasters)

📂 Project Structure
CarbonMarketTycoon/
 ├── Assets/
 │   ├── Scripts/
 │   │   ├── GameManager.cs        // Controls main loop
 │   │   ├── CompanyManager.cs     // Player company logic
 │   │   ├── MarketManager.cs      // Carbon credit trading system
 │   │   ├── DisasterManager.cs    // Triggers climate disasters
 │   │   ├── UIController.cs       // Handles scoreboards & menus
 │   ├── Prefabs/                  // Factories, renewable plants, etc.
 │   ├── UI/                       // Canvas elements
 │   └── Scenes/
 │       ├── MainMenu.unity
 │       ├── GameScene.unity
 │       └── GameOver.unity
 ├── README.md
 └── .gitignore

🧩 Sample Code Snippets
GameManager.cs
```csharp
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
```

CompanyManager.cs
```csharp
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
```

📊 Example Trial Run (Fake Data)

Year 1: Build 2 factories → Profit = $2000, Emissions = 100, Temp = 1.01°C

Year 5: Buy 3 renewables → Profit = $3500, Emissions = 115, Temp = 1.06°C

Year 10: Ignore climate → Profit = $8000, Emissions = 500, Temp = 2.5°C → 🌪️ disaster triggered → profit drops to $6400

🧑‍💻 How to Run

Clone repo:
```
git clone https://github.com/YOURUSERNAME/CarbonMarketTycoon.git
cd CarbonMarketTycoon
```

Open in Unity Hub (2021+).

Run GameScene.unity from Scenes/.

Play the simulation and try to survive without destroying the planet 🌍.

🎯 Recruiter Surprise Factor

✅ Tackles climate change economics, a real-world challenge
✅ Combines gamification + simulation + ethics
✅ Built in Unity + C#, widely used in both gaming & XR industries
✅ Conversation starter: "How would YOU balance profits vs sustainability?"
