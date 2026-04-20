using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro; // Добавляем для работы с текстом

public class FancyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _buttonImage;
    private Vector3 _initialScale;
    private TMP_Text _buttonText;

    [Header("Цвета (Серо-синяя гамма)")]
    // Спокойный серо-синий
    public Color normalColor = new Color(0.36f, 0.42f, 0.5f);
    // Мягкий светлый оттенок при наведении (не яркий)
    public Color hoverColor = new Color(0.45f, 0.52f, 0.6f);
    public float colorTransitionSpeed = 6f;

    [Header("Анимация")]
    public float scaleMultiplier = 1.05f; // Легкое расширение
    public float animationSpeed = 8f;
    public float clickSqueeze = 0.96f;   // Незначительное сжатие при клике

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _buttonText = GetComponentInChildren<TMP_Text>();
        _initialScale = transform.localScale;

        if (_buttonImage != null)
            _buttonImage.color = normalColor;

        // Делаем шрифт чуть толще через код, если используется TextMeshPro
        if (_buttonText != null)
        {
            _buttonText.fontWeight = FontWeight.Medium; // Мягкая толщина
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateButton(_initialScale * scaleMultiplier, hoverColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateButton(_initialScale, normalColor));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = _initialScale * clickSqueeze;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = _initialScale * scaleMultiplier;
    }

    private IEnumerator AnimateButton(Vector3 targetScale, Color targetColor)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
            if (_buttonImage != null)
            {
                _buttonImage.color = Color.Lerp(_buttonImage.color, targetColor, Time.deltaTime * colorTransitionSpeed);
            }
            yield return null;
        }
        transform.localScale = targetScale;
    }
}