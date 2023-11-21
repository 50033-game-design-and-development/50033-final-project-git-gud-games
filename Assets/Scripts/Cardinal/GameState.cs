using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Inv.Collectable> startInventory = new();
    public static readonly List<Inv.Collectable> inventory = new();

    public static Inv.Collectable? selectedInventoryItem = null; 
    public static bool isDraggingInventoryItem = false;
    public static Vector2 lastPointerDragScreenPos;
    public static bool mouseHold;
    
    private static bool _inventoryOpened = false;
    // whether or not the camera is locked onto a puzzle or not
    public static bool isPuzzleLocked = false;
    
    public static bool isDraggable => isPuzzleLocked || inventoryOpened;
    
    public static bool inventoryOpened {
        get => _inventoryOpened;
        set {
            _inventoryOpened = value;
            if (_inventoryOpened && inventory.Count > 0) {
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
