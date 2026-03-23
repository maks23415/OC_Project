using UnityEngine;
using TMPro;

public class MainMenuProgress : MonoBehaviour
{
    public TMP_Text progressText;

    void Start()
    {
        UpdateProgress();
    }

    void Update()
    {
        UpdateProgress();
    }

    void UpdateProgress()
    {
        // Проверяем, существует ли LevelManager
        if (LevelManager.Instance == null)
        {
            // Если LevelManager нет, показываем сообщение или ничего не делаем
            if (progressText != null)
            {
                progressText.text = "Загрузка...";
            }
            return;
        }

        // Проверяем, назначен ли текст
        if (progressText != null)
        {
            int maxLevel = LevelManager.Instance.GetMaxUnlockedLevel();
            progressText.text = "Доступно уровней: " + maxLevel;
        }
    }
}