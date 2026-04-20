using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLevel = 1;
    private int maxUnlockedLevel = 1;

    [Header("Настройки уровней")]
    public int maxLevelsCount = 3; // Укажи здесь максимальное количество уровней!

    [Header("Сброс (только для тестов)")]
    public bool forceReset = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadProgress()
    {
        if (forceReset)
        {
            maxUnlockedLevel = 1;
            PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
            PlayerPrefs.Save();
            Debug.Log("Прогресс принудительно сброшен! Открыт только уровень 1");
            forceReset = false;
            return;
        }

        if (PlayerPrefs.HasKey("MaxUnlockedLevel"))
        {
            maxUnlockedLevel = PlayerPrefs.GetInt("MaxUnlockedLevel");
            Debug.Log("Загружен сохранённый прогресс: открыто уровней - " + maxUnlockedLevel);
        }
        else
        {
            maxUnlockedLevel = 1;
            PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
            PlayerPrefs.Save();
            Debug.Log("Первый запуск игры: открыт уровень 1");
        }
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("MaxUnlockedLevel", maxUnlockedLevel);
        PlayerPrefs.Save();
        Debug.Log("Прогресс сохранён: максимальный открытый уровень - " + maxUnlockedLevel);
    }

    public void LoadLevel(int levelNumber)
    {
        // Защита от загрузки несуществующих уровней
        if (levelNumber > maxLevelsCount)
        {
            Debug.Log("Все уровни пройдены! Возвращаемся в меню.");
            SceneManager.LoadScene("MainMenu"); // Замени на сцену победы, если она есть
            return;
        }

        Debug.Log("Попытка загрузить уровень " + levelNumber + ". Открыто уровней: " + maxUnlockedLevel);

        if (levelNumber <= maxUnlockedLevel)
        {
            currentLevel = levelNumber;

            if (levelNumber == 1)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                SceneManager.LoadScene("Level" + levelNumber);
            }
        }
        else
        {
            Debug.Log("Уровень " + levelNumber + " ещё не открыт!");
        }
    }

    public void LoadNextLevel()
    {
        int nextLevel = currentLevel + 1;

        Debug.Log("Завершён уровень " + currentLevel + ". Пытаемся открыть следующий: " + nextLevel);

        if (nextLevel > maxUnlockedLevel && nextLevel <= maxLevelsCount)
        {
            maxUnlockedLevel = nextLevel;
            SaveProgress();
            Debug.Log("Открыт новый уровень " + nextLevel + "!");
        }

        LoadLevel(nextLevel);
    }

    public void RestartLevel()
    {
        if (currentLevel == 1)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
    }

    public void ResetProgress()
    {
        maxUnlockedLevel = 1;
        currentLevel = 1;
        PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
        PlayerPrefs.Save();
        Debug.Log("Прогресс полностью сброшен! Открыт только уровень 1");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetMaxUnlockedLevel()
    {
        return maxUnlockedLevel;
    }

    // Добавь это в LevelManager.cs
    public void SaveNewProgress(int levelToUnlock)
    {
        if (levelToUnlock > maxUnlockedLevel && levelToUnlock <= maxLevelsCount)
        {
            maxUnlockedLevel = levelToUnlock;
            PlayerPrefs.SetInt("MaxUnlockedLevel", maxUnlockedLevel);
            PlayerPrefs.Save();
            Debug.Log("В фоне разблокирован уровень: " + levelToUnlock);
        }
    }
}