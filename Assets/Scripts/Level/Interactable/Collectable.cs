using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public GameEvent onInventoryUpdate;
    public Sprite invSprite;
    public InventoryItems itemType;

    private Inv.Collectable invItem {
        get => new Inv.Collectable {
            itemType = itemType,
            itemSprite = invSprite,
        };
    }

    // TODO:
    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);

        GameState.inventory.Add(invItem);
        onInventoryUpdate.Raise();
        Destroy(gameObject);
    }
}