using UnityEngine;

public class Paperclip : MonoBehaviour
{
    public int value = 2; // Сколько стоит одна скрепка

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что скрепку задел именно самолетик (игрок)
        if (collision.CompareTag("player"))
        {
            // Отправляем деньги в банк
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddClips(value);
            }

            // Удаляем скрепку со сцены
            Destroy(gameObject);
        }
    }
}