using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Shop/Skin Item")]
public class SkinItem : ScriptableObject
{
    public string skinName;
    public Sprite skinSprite;
    public int price;
    public string skinID; // ”никальный ID (например, "skin_plane_red")
}
