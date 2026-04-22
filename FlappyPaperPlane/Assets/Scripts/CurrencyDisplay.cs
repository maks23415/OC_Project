using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    public TMP_Text currencyText;

    private void Start()
    {
        UpdateUI();
    }

    // ¬ызываем это в OnEnable, чтобы баланс обновл€лс€ при каждом открытии меню/магазина
    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (CurrencyManager.Instance != null && currencyText != null)
        {
            currencyText.text = CurrencyManager.Instance.GetTotalClips().ToString();
        }
    }
}
