using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusable : MonoBehaviour, IInteractable {
    
    public Animator cinemachineAnimator;
    public GameEvent onInventoryUpdate;
    
    // name of the virtual camera state in cinemachineAnimator to play when object is clicked
    public string startStateName;
    
    // name of the virtual camera state in cinemachineAnimator to play when player presses escape
    public string endStateName;
    
    public virtual void OnInteraction() {
        cinemachineAnimator.Play(startStateName);
        if (!GameState.inventoryOpened) {
            GameState.ToggleInventory();
            onInventoryUpdate.Raise();
            GameState.ConfineCursor();
            GameState.isFocused = true;
        }
    }
    
    private void OnEscape() {
        cinemachineAnimator.Play(endStateName);
        if (GameState.inventoryOpened) {
            GameState.ToggleInventory();
            onInventoryUpdate.Raise();
            GameState.LockCursor();
            GameState.isFocused = false;
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
