using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public SkinItem skin;
    public Image previewImage;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text buttonText;
    public Button actionButton;

    private ShopManager shopManager;

    public void Setup(SkinItem newSkin, ShopManager manager)
    {
        skin = newSkin;
        shopManager = manager;

        previewImage.sprite = skin.skinSprite;
        nameText.text = skin.skinName;
        UpdateUI();
    }

    public void UpdateUI()
    {
        bool isBought = PlayerPrefs.GetInt(skin.skinID + "_Bought", 0) == 1;
        bool isSelected = PlayerPrefs.GetString("SelectedSkin", "default") == skin.skinID;

        if (isSelected)
        {
            buttonText.text = "Выбрано";
            actionButton.interactable = false;
            priceText.text = "---";
        }
        else if (isBought)
        {
            buttonText.text = "Использовать";
            actionButton.interactable = true;
            priceText.text = "Куплено";
        }
        else
        {
            buttonText.text = "Купить";
            actionButton.interactable = true;
            priceText.text = skin.price.ToString() + " скрепок";
        }
    }

    public void OnClick()
    {
        shopManager.HandleClick(this);
    }
}