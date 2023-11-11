using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockFocusable : MonoBehaviour {
    public Animator cinemachineAnimator;
    public GameEvent onInventoryUpdate;
    
    public virtual void OnInteraction() {
        cinemachineAnimator.Play("L0 Door Lock");
        if (!GameState.inventoryOpened) {
            GameState.ToggleInventory();
            onInventoryUpdate.Raise();
            GameState.ConfineCursor();
        }
    }
    
    public void OnEscape() {
        cinemachineAnimator.Play("L0 First Person");
        if (GameState.inventoryOpened) {
            GameState.ToggleInventory();
            onInventoryUpdate.Raise();
            GameState.LockCursor();
        }
    }
    
    private void Start() {
        
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnEscape();
        }
    }
}
