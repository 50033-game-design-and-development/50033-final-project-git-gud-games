using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryItems {
    Ball = 1,
    Paper = 2
}

public class GameState: MonoBehaviour {
    public static readonly List<Collectable> Inventory = new List<Collectable>{};
    // initial list of items to assign to inventory (for testing only)
    public List<Collectable> startInventory = new List<Collectable>();
    private static bool _inventoryOpened = false;

    public static void ShowInventory() {
        _inventoryOpened = true;
    }

    public static void HideInventory() {
        _inventoryOpened = false;
    }

    public static bool IsInventoryOpened() {
        return _inventoryOpened;
    }
    
    void Start() {
        foreach (var collectable in startInventory) {
            Inventory.Add(collectable);
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
    
    // Update is called once per frame
    void Update() {
        
    }
}
