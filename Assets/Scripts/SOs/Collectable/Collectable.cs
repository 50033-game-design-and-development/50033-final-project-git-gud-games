using UnityEngine;

// Create a menu item for creating the ScriptableObject asset
[CreateAssetMenu(
    fileName = "Collectable",
    menuName = "Inventory/Collectable",
    order = 1
)]
public class Collectable : ScriptableObject {
    public InventoryItems itemType; // The enum member
    public Sprite itemSprite; // The sprite member
}
