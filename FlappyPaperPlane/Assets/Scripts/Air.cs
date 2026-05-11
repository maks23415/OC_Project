using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float rotatePower;
    public float jumpSpeed;

    [Header("Настройки скорости")]
    public float speed; // Это будет стандартная скорость
    public List<float> speedsPerLevel = new List<float> { 5f, 7f, 10f }; // Список скоростей для уровней

    public AudioClip jumpSound;

    private AudioSource source;
    private Rigidbody2D rb;

    private void Start()
    {
        //  Сначала определяем скорость в зависимости от уровня
        SetSpeedByLevel();

        // Передаем итоговую скорость в твой глобальный скрипт движения объектов
        Object.speed = speed;

        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetSpeedByLevel()
    {
        // Проверяем, существует ли LevelManager
        if (LevelManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel(); 
            int index = currentLevel - 1; // Индекс в списке (для 1 уровня это 0)

            // Если для этого уровня прописана скорость в списке берем её
            if (index >= 0 && index < speedsPerLevel.Count)
            {
                speed = speedsPerLevel[index];
                Debug.Log("Установлена скорость для уровня " + currentLevel + ": " + speed);
            }
            else
            {
                Debug.LogWarning("Скорость для уровня " + currentLevel + " не настроена в списке! Использую стандартную: " + speed);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.PlayOneShot(jumpSound, 0.3f);
            rb.linearVelocity = Vector2.up * jumpSpeed;
        }
        transform.eulerAngles = new Vector3(0, 0, rb.linearVelocity.y * rotatePower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") || collision.CompareTag("wardrobe"))
        {
            Score.score++;
        }
    }
}