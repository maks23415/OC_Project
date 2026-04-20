using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static int score;
    public TMP_Text scoreText;
    public int requiredScore = 10;

    private bool isLevelCompleted = false;

    private void Start()
    {
        score = 0;
        isLevelCompleted = false;
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        if (score >= requiredScore && !isLevelCompleted)
        {
            isLevelCompleted = true;
            OnLevelWin();
        }
    }

    void OnLevelWin()
    {
        // 1. Сначала просто открываем следующий уровень в прогрессе (сохраняем)
        int currentLevel = LevelManager.Instance.GetCurrentLevel();
        int nextLevel = currentLevel + 1;

        if (nextLevel <= LevelManager.Instance.maxLevelsCount)
        {
            // Разблокируем его в памяти, но НЕ загружаем сразу
            if (nextLevel > LevelManager.Instance.GetMaxUnlockedLevel())
            {
                LevelManager.Instance.SaveNewProgress(nextLevel);
            }
        }

        // 2. А теперь решаем, что делать дальше. 
        // Вместо автоматического LoadNextLevel(), давай возвращать игрока в меню
        // или показывать панель победы.

        Debug.Log("Уровень пройден! Прогресс сохранен. Возвращаемся в меню...");
        SceneManager.LoadScene("MainMenu");
    }
}