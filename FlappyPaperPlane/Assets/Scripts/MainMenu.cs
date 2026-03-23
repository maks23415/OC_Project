using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // При запуске главного меню выводим информацию о прогрессе
        if (LevelManager.Instance != null)
        {
            Debug.Log("Текущий прогресс: разблокировано уровней - " + LevelManager.Instance.GetMaxUnlockedLevel());
        }
    }

    public void StartGame()
    {
        // Загружаем первый уровень
        SceneManager.LoadScene("Game");
    }

    public void StartGameWithManager()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(1);
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    // Кнопка для сброса прогресса
    public void ResetGameProgress()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetProgress();
            // Перезагружаем сцену, чтобы обновить кнопки
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.LogError("LevelManager не найден!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Выход из игры");
    }
}