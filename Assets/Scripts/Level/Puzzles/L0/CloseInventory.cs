using UnityEngine;

public class CloseInventory : MonoBehaviour {
    public void Close() {
        GameState.isInventoryOpened = false;
        Event.Global.inventoryUpdate.Raise();
    }
}
