using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIStyleManager : MonoBehaviour
{
    // Цветовая палитра "Slate & Bone"
    private readonly Color colorNormal = new Color(0.29f, 0.38f, 0.45f); // Спокойный сине-серый
    private readonly Color colorHover = new Color(0.35f, 0.45f, 0.53f); // Светлее при наведении
    private readonly Color colorPressed = new Color(0.22f, 0.30f, 0.36f); // Темнее при нажатии
    private readonly Color colorText = new Color(0.95f, 0.93f, 0.88f); // Мягкий бежевый (текст)

    public void ApplyStyleToButton(GameObject btnObj, string label)
    {
        // Настройка фона
        Image img = btnObj.GetComponent<Image>();
        if (img == null) img = btnObj.AddComponent<Image>();
        img.color = colorNormal;
        img.raycastTarget = true;

        // Настройка текста
        GameObject txtObj = btnObj.transform.Find("Text")?.gameObject;
        if (txtObj == null)
        {
            txtObj = new GameObject("Text");
            txtObj.transform.SetParent(btnObj.transform, false);
        }

        var txt = txtObj.GetComponent<TextMeshProUGUI>();
        if (txt == null) txt = txtObj.AddComponent<TextMeshProUGUI>();

        txt.text = label;
        txt.color = colorText;
        txt.fontSize = 28;
        txt.alignment = TextAlignmentOptions.Center;

        // ДЕЛАЕМ ШРИФТ ТОЛЩЕ
        txt.fontStyle = FontStyles.Bold; // Включаем жирное начертание

        // Добавляем логику анимации (как в предыдущем примере)
        SetupTransitions(btnObj, img);
    }

    private void SetupTransitions(GameObject obj, Image img)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        RectTransform rt = obj.GetComponent<RectTransform>();

        // Наведение (Увеличение + Цвет)
        AddEvent(trigger, EventTriggerType.PointerEnter, () => {
            img.color = colorHover;
            rt.localScale = new Vector3(1.05f, 1.05f, 1f);
        });

        // Уход курсора (Сброс)
        AddEvent(trigger, EventTriggerType.PointerExit, () => {
            img.color = colorNormal;
            rt.localScale = Vector3.one;
        });

        // Нажатие (Сжатие + Темный цвет)
        AddEvent(trigger, EventTriggerType.PointerDown, () => {
            img.color = colorPressed;
            rt.localScale = new Vector3(0.95f, 0.95f, 1f);
        });
    }

    private void AddEvent(EventTrigger trigger, EventTriggerType type, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action.Invoke());
        trigger.triggers.Add(entry);
    }
}