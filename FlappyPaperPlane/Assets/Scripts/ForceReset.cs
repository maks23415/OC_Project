using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceReset : MonoBehaviour
{
    void Start()
    {
        // Полностью очищаем все сохранения
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("======================");
        Debug.Log("ВСЕ СОХРАНЕНИЯ ОЧИЩЕНЫ!");
        Debug.Log("Теперь доступен только уровень 1");
        Debug.Log("======================");

        // Удаляем этот объект
        Destroy(gameObject);

        // Перезагружаем сцену, чтобы обновить кнопки
        SceneManager.LoadScene("MainMenu");
    }
}
