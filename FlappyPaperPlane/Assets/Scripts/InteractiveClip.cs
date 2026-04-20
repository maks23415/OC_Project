using UnityEngine;

public class InteractiveClip : MonoBehaviour
{
    [Header("Настройки бонуса")]
    public int bonusPoints = 2; // Сколько очков даем

    [Header("Настройки звука")]
    public AudioClip collectSound; 
    [Range(0f, 1f)]
    public float volume = 0.5f;    // Громкость звука

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<Air>() != null)
        {
            // Воспроизводим звук
            if (collectSound != null)
            {
                // Создает временный объект со звуком в точке скрепки
                // Это важно, так как обычный AudioSource на скрепке исчезнет вместе с ней
                AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            }

            // Добавляем очки в общую копилку
            Score.score += bonusPoints;

            //  Компенсируем порог очков
            Score scoreScript = Object.FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.requiredScore += bonusPoints;
            }

            // Удаляем скрепку
            Destroy(gameObject);

            Debug.Log($"Скрепка разрушена со звуком! +{bonusPoints} очков.");
        }
    }
}