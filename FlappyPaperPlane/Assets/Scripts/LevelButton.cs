using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public TMP_Text levelText; // Сделайте это поле НЕ обязательным
    public GameObject lockIcon; // Сделайте это поле НЕ обязательным

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("На объекте " + gameObject.name + " нет компонента Button!");
            return;
        }

        UpdateButtonState();
        button.onClick.AddListener(OnButtonClick);
    }

    void UpdateButtonState()
    {
        // Проверяем, существует ли LevelManager
        if (LevelManager.Instance == null)
        {
            Debug.LogWarning("LevelManager.Instance не найден!");
            return;
        }

        bool isUnlocked = levelNumber <= LevelManager.Instance.GetMaxUnlockedLevel();

        // Обновляем кнопку
        if (button != null)
        {
            button.interactable = isUnlocked;
        }

        // Обновляем текст ТОЛЬКО если он назначен
        if (levelText != null)
        {
            if (isUnlocked)
            {
                levelText.color = Color.white;
                levelText.text = "Уровень " + levelNumber;
            }
            else
            {
                levelText.color = Color.gray;
                levelText.text = "???";
            }
        }

        // Обновляем иконку замка ТОЛЬКО если она назначена
        if (lockIcon != null)
        {
            lockIcon.SetActive(!isUnlocked);
        }
    }

    void OnButtonClick()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(levelNumber);
        }
        else
        {
            Debug.LogError("LevelManager.Instance == null!");
        }
    }

    private void OnEnable()
    {
        UpdateButtonState();
    }
}