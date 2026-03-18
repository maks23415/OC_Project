using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Загружаем сцену с игрой
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        // Выход из игры
        Application.Quit();
        Debug.Log("Выход из игры"); // Для тестирования в редакторе
    }
}
