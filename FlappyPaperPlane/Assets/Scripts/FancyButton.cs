using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro; // ƒобавл€ем дл€ работы с текстом

public class FancyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _buttonImage;
    private Vector3 _initialScale;
    private TMP_Text _buttonText;

    [Header("÷вета (€рка€ гамма)")]
    // —покойный серо-синий
    public Color normalColor = Color.white;
    // ћ€гкий светлый оттенок при наведении (не €ркий)
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public float colorTransitionSpeed = 6f;

    [Header("јнимаци€")]
    public float scaleMultiplier = 1.05f; // Ћегкое расширение
    public float animationSpeed = 8f;
    public float clickSqueeze = 0.96f;   // Ќезначительное сжатие при клике

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _buttonText = GetComponentInChildren<TMP_Text>();
        _initialScale = transform.localScale;

        if (_buttonImage != null)
            _buttonImage.color = normalColor;

        // ƒелаем шрифт чуть толще через код, если используетс€ TextMeshPro
        if (_buttonText != null)
        {
            _buttonText.fontWeight = FontWeight.Medium; // ћ€гка€ толщина
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