using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance; // simple singleton so any script can call CurrencyManager.Instance.AddMoney(x)

    [Header("UI")]
    public TMP_Text moneyText;

    private int currentMoney = 0;

    void Awake()
    {
        // Basic singleton guard - if one already exists, destroy this duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + currentMoney;
        }
    }
}