using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLevel = 1;
    private int maxUnlockedLevel = 1;

    // Добавьте эту переменную для форсированного сброса
    public bool forceReset = false; // Временно включите true для сброса

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
        // ФОРСИРОВАННЫЙ СБРОС - включите это один раз
        if (forceReset)
        {
            maxUnlockedLevel = 1;
            PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
            PlayerPrefs.Save();
            Debug.Log("Форсированный сброс выполнен! Доступен только уровень 1");
            forceReset = false; // Отключаем после сброса
            return;
        }

        // Обычная загрузка
        if (PlayerPrefs.HasKey("MaxUnlockedLevel"))
        {
            maxUnlockedLevel = PlayerPrefs.GetInt("MaxUnlockedLevel");
            Debug.Log("Загружен сохраненный прогресс: уровень " + maxUnlockedLevel);
        }
        else
        {
            maxUnlockedLevel = 1;
            PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
            PlayerPrefs.Save();
            Debug.Log("Создан новый прогресс: доступен только уровень 1");
        }
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("MaxUnlockedLevel", maxUnlockedLevel);
        PlayerPrefs.Save();
        Debug.Log("Прогресс сохранен: разблокировано уровней - " + maxUnlockedLevel);
    }

    public void LoadLevel(int levelNumber)
    {
        Debug.Log("Попытка загрузить уровень " + levelNumber + ". Доступно уровней: " + maxUnlockedLevel);

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
            Debug.Log("Уровень " + levelNumber + " еще не разблокирован!");
        }
    }

    public void LoadNextLevel()
    {
        int nextLevel = currentLevel + 1;

        Debug.Log("Завершен уровень " + currentLevel + ". Пытаемся разблокировать уровень " + nextLevel);

        if (nextLevel > maxUnlockedLevel)
        {
            maxUnlockedLevel = nextLevel;
            SaveProgress();
            Debug.Log("РАЗБЛОКИРОВАН уровень " + nextLevel + "!");
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

    // Сброс прогресса
    public void ResetProgress()
    {
        maxUnlockedLevel = 1;
        currentLevel = 1;
        PlayerPrefs.SetInt("MaxUnlockedLevel", 1);
        PlayerPrefs.Save();
        Debug.Log("Прогресс сброшен! Доступен только уровень 1");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetMaxUnlockedLevel()
    {
        return maxUnlockedLevel;
    }
}