using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Collectable> startInventory = new List<Collectable>();

    public static readonly List<Collectable> Inventory = new List<Collectable>();

    private static bool _inventoryOpened = false;

    public static bool InventoryOpened {
        get => _inventoryOpened;
        set {
            _inventoryOpened = value;
            if (_inventoryOpened) {
                ConfineCursor();
            } else {
                LockCursor();
            }
        }
    }
    
    public static void ToggleInventory() {
        InventoryOpened = !InventoryOpened;
    }
    
    public static bool IsInventoryOpened() {
        return _inventoryOpened;
    }

    public static void LockCursor() {
        // A locked cursor is positioned in the center
        // of the view and cannot be moved.
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ConfineCursor() {
        Debug.Log("ENABLE_CURSOR");
        // The cursor is visible and can be moved around
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start() {
        foreach (var collectable in startInventory) {
            Inventory.Add(collectable);
        }
    }
}
