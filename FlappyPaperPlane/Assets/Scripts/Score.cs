using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score;
    public TMP_Text scoreText;
    public int requiredScore = 10; // Нужно набрать 10 очков

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = score.ToString();

        // Проверяем, набрано ли 10 очков
        if (score >= requiredScore)
        {
            // Переходим на следующий уровень
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.LoadNextLevel();
            }
        }
    }
}
