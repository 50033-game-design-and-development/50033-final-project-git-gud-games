using System;
using UnityEngine;

public class CameraFocusable : MonoBehaviour, IInteractable {
    
    public Animator cinemachineAnimator;
    
    // name of the virtual camera state in cinemachineAnimator to play when object is clicked
    public string startStateName;
    
    // name of the virtual camera state in cinemachineAnimator to play when player presses escape
    public string endStateName;

    public PlayerAction _playerAction;

    public bool IsCinemachineInStartState() {
        AnimatorStateInfo stateInfo = cinemachineAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(startStateName);
    }
  
    public virtual void OnInteraction() {
        if (GameState.isPuzzleLocked) {
            // dont use the camera to move the CineMachine
            // to an interactable object if camera is locked onto a puzzle
            return;
        }
        
        cinemachineAnimator.Play(startStateName);
        GameState.isPuzzleLocked = true;
        GameState.ConfineCursor();
        Event.Global.changeCamera.Raise();
    }
    
    public virtual void OnEscape() {
        if (!GameState.isPuzzleLocked) {
            return;
        }
        cinemachineAnimator.Play(endStateName);
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;
        GameState.LockCursor();
        Event.Global.changeCamera.Raise();
    }

    public void Start() {
        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.Escape.performed += _ => OnEscape();
    }

    public void OnDisable() {
        _playerAction.Disable();
    }
}
