using UnityEngine;

[CreateAssetMenu(
    fileName = "Collectable",
    menuName = "Inventory/Collectable",
    order = 1
)]
public class Collectable : ScriptableObject {
    public InventoryItems itemType;
    public Sprite itemSprite;
}
