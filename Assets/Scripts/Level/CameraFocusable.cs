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
    
    private bool IsCinemachineInStartState() {
        AnimatorStateInfo stateInfo = cinemachineAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(startStateName);
    }
  
    public virtual void OnInteraction() {
        if (GameState.inventoryOpened) {
            // dont use the camera to move the CineMachine
            // to an interactable object if inventory is already opened
            return;
        }
        
        cinemachineAnimator.Play(startStateName);
        GameState.inventoryOpened = true;
        onInventoryUpdate.Raise();
    }
    
    private void OnEscape() {
        cinemachineAnimator.Play(endStateName);
        GameState.inventoryOpened = false;
        onInventoryUpdate.Raise();
    }
    
    private void Update() {
        if (
            Input.GetKeyDown(KeyCode.Escape) &&
            IsCinemachineInStartState()
        ) {
            OnEscape();
        }
    }
}
