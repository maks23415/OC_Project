using UnityEngine;
using System.Collections; // Обязательно добавь эту строку для работы Корутин

public class InteractiveClip : MonoBehaviour
{
    [Header("Настройки бонуса")]
    public int bonusPoints = 2; // Сколько очков даем

    [Header("Настройки звука")]
    public AudioClip collectSound; // Перетащи аудиофайл в инспекторе
    [Range(0f, 1f)]
    public float volume = 0.5f;    // Громкость звука

    [Header("Настройки эффекта разрушения")]
    public float animationDuration = 1.0f; // Длительность анимации исчезновения
    public float upwardSpeed = 2.0f;       // Скорость взлета вверх

    // Переменные для работы с компонентами
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private bool _isCollected = false; // Флаг, чтобы эффект не сработал дважды

    private void Start()
    {
        // Получаем ссылки на компоненты при старте
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Проверка на ошибки
        if (_spriteRenderer == null)
        {
            Debug.LogError($"На объекте {gameObject.name} нет SpriteRenderer! Эффект исчезновения не сработает.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если уже собрано или врезался не игрок — выходим
        if (_isCollected) return;

        if (other.CompareTag("Player") || other.GetComponent<Air>() != null)
        {
            _isCollected = true; // Помечаем как собранное

            // Воспроизводим звук
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            }

            // Логика очков
            Score.score += bonusPoints;
            Score scoreScript = Object.FindFirstObjectByType<Score>();
            if (scoreScript != null)
            {
                scoreScript.requiredScore += bonusPoints;
            }

            // ЗАПУСКАЕМ ЭФФЕКТЫ
            StartCoroutine(DestructionAnimation());

            Debug.Log($"Булавка собрана! Запущен эффект разрушения.");
        }
    }

    // Это Корутина — функция, которая умеет выполнять действия по времени
    private IEnumerator DestructionAnimation()
    {
        // Сразу отключаем коллайдер, чтобы булавка больше не взаимодействовала ни с чем
        if (_collider != null)
        {
            _collider.enabled = false;
        }

        // Если нет спрайта, просто удаляем объект и выходим
        if (_spriteRenderer == null)
        {
            Destroy(gameObject);
            yield break;
        }

        // Подготовка к циклу анимации
        float elapsedTime = 0f; // Прошедшее время
        Color originalColor = _spriteRenderer.color; // Запоминаем начальный цвет

        // ЦИКЛ АНИМАЦИИ (выполняется каждый кадр в течение animationDuration)
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime; // Увеличиваем время
            float progress = elapsedTime / animationDuration; // Прогресс от 0 до 1

            // Движение вверх (меняем позицию)
            transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime, Space.World);

            // Постепенное исчезновение (меняем Альфа-канал цвета)
            // Mathf.Lerp плавно меняет значение от 1 (видимый) до 0 (прозрачный)
            float newAlpha = Mathf.Lerp(1f, 0f, progress);
            _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            // Ждем следующего кадра
            yield return null;
        }

        // Удаляем объект, когда он стал полностью невидимым
        Destroy(gameObject);
    }
}