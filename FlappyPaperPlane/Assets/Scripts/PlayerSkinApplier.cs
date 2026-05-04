using UnityEngine;
using System.Collections.Generic;

public class PlayerSkinApplier : MonoBehaviour
{
    [Header("Настройки ссылок")]
    public SpriteRenderer spriteRenderer;
    public List<SkinItem> allSkins;

    [Header("Точные настройки из инспектора")]
    public bool applyExactTransform = true;

    // Твои значения Position
    private Vector3 targetPosition = new Vector3(-0.1443f, -0.2258f, 1f);
    // Твои значения Rotation (Z = 1)
    private Vector3 targetRotation = new Vector3(0f, 0f, 1f);
    // Твои значения Scale
    private Vector3 targetScale = new Vector3(0.32f, 0.32f, 0.4f);

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ApplySkin();

        if (applyExactTransform)
        {
            // Устанавливаем всё в точности как ты указал
            transform.localPosition = targetPosition;
            transform.localRotation = Quaternion.Euler(targetRotation);
            transform.localScale = targetScale;
        }
    }

    public void ApplySkin()
    {
        string selectedID = PlayerPrefs.GetString("SelectedSkin", "default");
        SkinItem currentSkin = allSkins.Find(s => s.skinID == selectedID);

        if (currentSkin != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = currentSkin.skinSprite;
            Debug.Log($"[SkinSystem] Скин {selectedID} применен с твоими точными размерами.");
        }
    }
}