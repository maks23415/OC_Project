using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Банк должен работать на всех сценах
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Получить общее количество скрепок из памяти
    public int GetTotalClips()
    {
        return PlayerPrefs.GetInt("TotalPaperclips", 0);
    }

    // Добавить скрепки в "кошелек"
    public void AddClips(int amount)
    {
        int currentBalance = GetTotalClips();
        int newBalance = currentBalance + amount;
        PlayerPrefs.SetInt("TotalPaperclips", newBalance);
        PlayerPrefs.Save();
        Debug.Log("Скрепка добавлена! Новый баланс: " + newBalance);
    }
}
