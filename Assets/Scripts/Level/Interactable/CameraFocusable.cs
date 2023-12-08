using UnityEngine;

public class CameraFocusable : MonoBehaviour, IInteractable {
    
    public Animator cinemachineAnimator;
    
    // name of the virtual camera state in cinemachineAnimator to play when object is clicked
    public string startStateName;
    
    // name of the virtual camera state in cinemachineAnimator to play when player presses escape
    public string endStateName;

    protected PlayerAction _playerAction;

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
        if(!GameState.isCutscenePlaying) {
            GameState.ConfineCursor();
        }
        Event.Global.changeCamera.Raise();
    }
    
    public virtual void OnEscape() {
        if (!GameState.isPuzzleLocked || GameState.isCutscenePlaying) {
            return;
        }
        cinemachineAnimator.Play(endStateName);
        GameState.isPuzzleLocked = false;
        GameState.wasPuzzleLocked = true;
        GameState.LockCursor();
        Event.Global.changeCamera.Raise();
    }

    protected void Start() {
        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.Escape.performed += _ => OnEscape();
    }

    protected void OnDisable() {
        _playerAction.Disable();
    }
}
