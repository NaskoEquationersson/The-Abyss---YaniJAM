using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public string description;
    public int price;
    public bool isPurchased;
    public GameObject linkedTool; // the tool GameObject/script this unlocks, enabled on purchase
}

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    public ShopItem[] items;

    [Header("UI References (one row per item, same order as items[])")]
    public TMP_Text[] nameLabels;
    public TMP_Text[] descLabels;
    public TMP_Text[] priceLabels;
    public Button[] buyButtons;

    void OnEnable()
    {
        RefreshShopUI();
    }

    public void BuyItem(int index)
    {
        if (index < 0 || index >= items.Length) return;

        ShopItem item = items[index];

        if (!item.isPurchased && CurrencyManager.Instance != null && CurrencyManager.Instance.GetMoney() >= item.price)
        {
            CurrencyManager.Instance.SpendMoney(item.price);
            item.isPurchased = true;

            if (item.linkedTool != null)
            {
                item.linkedTool.SetActive(true); // unlock the tool
            }

            RefreshShopUI();
        }
    }

    void RefreshShopUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (nameLabels.Length > i) nameLabels[i].text = items[i].itemName;
            if (descLabels.Length > i) descLabels[i].text = items[i].description;
            if (priceLabels.Length > i) priceLabels[i].text = items[i].isPurchased ? "Owned" : "$" + items[i].price;
            if (buyButtons.Length > i) buyButtons[i].interactable = !items[i].isPurchased;
        }
    }
}