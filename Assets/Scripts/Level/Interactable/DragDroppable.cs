using System.Collections.Generic;
using UnityEngine;

public class DragDoppable : MonoBehaviour, IDragDroppable {
    public List<InventoryItem> possibleDroppable;
    public GameEvent @event;

    public bool retainItem = false;

    private HashSet<InventoryItem> _possibleDroppable;
    public void OnDragDrop() {
        if (GameState.selectedInventoryItem == null) {
            return;
        }
        var selectedInventoryItem = GameState.selectedInventoryItem.Value;
        if (!_possibleDroppable.Contains(selectedInventoryItem.itemType)) {
            return;
        }

        if(@event != null) {
            @event.Raise();
        }

        if(retainItem) return;

        GameState.inventory.Remove(selectedInventoryItem);
        Event.Global.inventoryUpdate.Raise();
    }

    private void Awake() {
        _possibleDroppable = new HashSet<InventoryItem>(possibleDroppable);
    }
}
