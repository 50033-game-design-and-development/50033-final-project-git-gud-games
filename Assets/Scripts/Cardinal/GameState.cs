using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour {
    // initial list of items to assign to inventory (for testing only)
    public List<Inv.Collectable> startInventory = new();
    public PlayerConstants playerConstants;
    public Save _save;

    public static float raycastDist;
    public static List<Inv.Collectable> inventory = new();
    public static Inv.Collectable? selectedInventoryItem = null; 
    public static bool isDraggingInventoryItem = false;
    public static Vector2 lastPointerDragScreenPos;
    public static bool mouseHold;
    public static bool isPaused;
    public static Queue<MonologueKey> instructionQueue = new();
    public static int level = 0;

    public static Save save;

    private static GameObject _pausedPanel;
    private static MonologueUI _monologueUI;

    private static bool _isInventoryOpened;
    // whether or not the camera is locked onto a puzzle or not
    public static bool isPuzzleLocked = false;
    public static bool wasPuzzleLocked = false;
    public static bool isCutscenePlaying = false;
    public static bool isInteractionAllowed => !(isPuzzleLocked || isInventoryOpened || isCutscenePlaying);
    
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
    
    /// <summary>
    /// toggle the inventory visibility
    /// </summary>
    /// <param name="adjustCursor">
    /// whether to automatically change the cursor
    /// lock state after toggling the inventory
    /// </param>
    public static void ToggleInventory(bool adjustCursor = true) {
        if (adjustCursor) {
            isInventoryOpened = !isInventoryOpened;
        } else {
            _isInventoryOpened = !_isInventoryOpened;
        }
        Event.Global.inventoryUpdate.Raise();
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

    public static void HidePauseMenuUiElements() {
        var children = _pausedPanel.GetComponentsInChildren<Transform>()
                       .Select(person => person.gameObject)
                       .Skip(1)
                       .ToArray();

        foreach (var child in children) {
            child.SetActive(false);
        }
    }

    public static void TogglePause(bool enablePanels = true) {
        isPaused = !isPaused;

        AudioListener.pause = isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;

        _monologueUI.TogglePanel(!isPaused && enablePanels);
        _pausedPanel.SetActive(isPaused || !enablePanels);

        Action AdjustCursor = isPaused ? ConfineCursor : LockCursor;
        AdjustCursor();
    }

    private void Start() {
        foreach (var collectable in startInventory) {
            inventory.Add(collectable);
        }

        raycastDist = playerConstants.raycastDistance;

        GameObject monologuePanel = GameObject.Find("MonologuePanel");
        if (monologuePanel != null) {
            _monologueUI = monologuePanel.GetComponent<MonologueUI>();
        }
        _pausedPanel = GameObject.Find("Paused");
        if(_pausedPanel != null) {
            _pausedPanel.SetActive(false);
        }

        if (_save != null) {
            save = _save;
        }
    }
}
