using System.Collections.Generic;
using UnityEngine;

public class DragDoppable : MonoBehaviour, IDragDroppable {
    public List<InventoryItem> possibleDroppable;
    public GameEvent @event;

    public bool retainItem;

    private HashSet<InventoryItem> _possibleDroppable;
    public void OnDragDrop() {
        if (GameState.selectedInventoryItem == null) {
            return;
        }
        var selectedInventoryItem = GameState.selectedInventoryItem.Value;
        if (!_possibleDroppable.Contains(selectedInventoryItem.itemType)) {
            Event.Global.showDialogue.Raise(MonologueKey.WRONG_ITEM);
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
        UpdateDroppables();
    }

    public void UpdateDroppables() {
        _possibleDroppable = new(possibleDroppable);
    }
}
