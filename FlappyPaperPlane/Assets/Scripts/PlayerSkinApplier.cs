using UnityEngine;
using System.Collections.Generic;

public class PlayerSkinApplier : MonoBehaviour
{
    [Header("Настройки ссылок")]
    public SpriteRenderer spriteRenderer;
    public List<SkinItem> allSkins;

    [Header("Точные настройки из твоего скриншота")]
    public bool applyExactTransform = true;

    private Vector3 targetPosition = new Vector3(-0.06f, -0.12f, 1f);

    private Vector3 targetRotation = new Vector3(0f, 0f, 1f);

    private Vector3 targetScale = new Vector3(0.3f, 0.5f, 0.5f);

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
            Debug.Log($"[SkinSystem] Скин {selectedID} применен с масштабом {targetScale}");
        }
    }
}