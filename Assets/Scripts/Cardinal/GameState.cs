using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Inv.Collectable> startInventory = new();
    public PlayerConstants playerConstants;

    public static float raycastDist;
    public static readonly List<Inv.Collectable> inventory = new();
    public static Inv.Collectable? selectedInventoryItem = null; 
    public static bool isDraggingInventoryItem = false;
    public static Vector2 lastPointerDragScreenPos;
    public static bool mouseHold;

    private static bool _isInventoryOpened;
    // whether or not the camera is locked onto a puzzle or not
    public static bool isPuzzleLocked = false;
    
    public static bool isInteractionAllowed => !(isPuzzleLocked || isInventoryOpened);
    
    public static bool isInventoryOpened {
        get => _isInventoryOpened;
        set {
            _isInventoryOpened = value;
            if (_isInventoryOpened && inventory.Count > 0) {
                ConfineCursor();
            } else {
                LockCursor();
            }
        }
    }
    
    public static void ToggleInventory(bool adjustCursor = true) {
        if (adjustCursor) {
            isInventoryOpened = !isInventoryOpened;
        } else {
            _isInventoryOpened = !_isInventoryOpened;
        }
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

        raycastDist = playerConstants.raycastDistance;
    }
}
