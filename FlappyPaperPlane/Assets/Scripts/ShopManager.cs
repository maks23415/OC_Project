using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public List<SkinItem> allSkins; // Закинь сюда все свои ScriptableObjects
    public GameObject itemPrefab;   // Префаб карточки товара
    public Transform container;     // Куда складывать карточки (Content в ScrollView)

    private List<ShopItemUI> uiItems = new List<ShopItemUI>();

    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (var skin in allSkins)
        {
            GameObject obj = Instantiate(itemPrefab, container);
            ShopItemUI ui = obj.GetComponent<ShopItemUI>();
            ui.Setup(skin, this);
            uiItems.Add(ui);
        }
    }

    public void HandleClick(ShopItemUI item)
    {
        string skinID = item.skin.skinID;
        bool isBought = PlayerPrefs.GetInt(skinID + "_Bought", 0) == 1;

        if (isBought)
        {
            // Если куплено — выбираем
            PlayerPrefs.SetString("SelectedSkin", skinID);
        }
        else
        {
            // Пытаемся купить
            int balance = CurrencyManager.Instance.GetTotalClips();
            if (balance >= item.skin.price)
            {
                // Списываем деньги через твой CurrencyManager (добавь там метод SpendClips)
                PlayerPrefs.SetInt("TotalPaperclips", balance - item.skin.price);
                PlayerPrefs.SetInt(skinID + "_Bought", 1);
                PlayerPrefs.SetString("SelectedSkin", skinID);
            }
            else
            {
                Debug.Log("Недостаточно скрепок!");
                return;
            }
        }

        RefreshAllItems();
    }

    void RefreshAllItems()
    {
        foreach (var item in uiItems) item.UpdateUI();
    }
}