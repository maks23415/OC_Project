using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (LevelManager.Instance != null)
        {
            Debug.Log("Текущий прогресс: разблокировано уровней - " + LevelManager.Instance.GetMaxUnlockedLevel());
        }
    }

    public void StartGame()
    {
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

    // Кнопка для ПОЛНОГО сброса всего прогресса
    public void ResetGameProgress()
    {
        // 1. Стираем абсолютно все записи PlayerPrefs (валюта, покупки, ID скинов)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Принудительно сохраняем изменения

        // 2. Сбрасываем прогресс уровней через LevelManager (если он хранит что-то в переменных)
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetProgress();
        }

        Debug.Log("Весь прогресс, валюта и покупки сброшены!");

        // 3. Перезагружаем сцену, чтобы скрипты подхватили пустые значения
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Выход из игры");
    }
}