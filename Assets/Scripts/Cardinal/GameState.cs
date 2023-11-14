using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Inv.Collectable> startInventory = new();
    public static readonly List<Inv.Collectable> inventory = new();
    
    public static Inv.Collectable? selectedInventoryItem = null; 
    public static bool isDraggingInventoryItem = false;
    public static Vector2 lastPointerDragScreenPos;
    public static bool mouseHold;
    public static bool isFocused;
    
    private static bool _inventoryOpened = false;
    
    public static bool inventoryOpened {
        get => _inventoryOpened;
        private set {
            _inventoryOpened = value;
            if (_inventoryOpened) {
                ConfineCursor();
            } else {
                LockCursor();
            }
        }
    }
    
    public static void ToggleInventory() {
        inventoryOpened = !inventoryOpened;
    }
    
    public static void LockCursor() {
        // A locked cursor is positioned in the center
        // of the view and cannot be moved.
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ConfineCursor() {
        // The cursor is visible and can be moved around
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start() {
        foreach (var collectable in startInventory) {
            inventory.Add(collectable);
        }
    }
}
