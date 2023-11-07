using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Collectable> startInventory = new List<Collectable>();

    public static readonly List<Collectable> inventory = new List<Collectable>();

    private static bool inventoryOpened = false;

    public static void ShowInventory() {
        inventoryOpened = true;
    }

    public static void HideInventory() {
        inventoryOpened = false;
    }

    public static bool IsInventoryOpened() {
        return inventoryOpened;
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
