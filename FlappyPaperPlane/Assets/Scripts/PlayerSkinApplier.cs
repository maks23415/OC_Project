using UnityEngine;
using System.Collections.Generic;

public class PlayerSkinApplier : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<SkinItem> allSkins;

    void Start()
    {
        ApplySkin();
    }

    public void ApplySkin()
    {
        string selectedID = PlayerPrefs.GetString("SelectedSkin", "default");
        SkinItem currentSkin = allSkins.Find(s => s.skinID == selectedID);

        if (currentSkin != null)
        {
            spriteRenderer.sprite = currentSkin.skinSprite;
        }
    }
}