using UnityEngine;

public class CameraFocusable : MonoBehaviour, IInteractable {
    
    public Animator cinemachineAnimator;
    
    // name of the virtual camera state in cinemachineAnimator to play when object is clicked
    public string startStateName;
    
    // name of the virtual camera state in cinemachineAnimator to play when player presses escape
    public string endStateName;
    
    private bool IsCinemachineInStartState() {
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
    }
    
    private void OnEscape() {
        cinemachineAnimator.Play(endStateName);
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;
        Event.Global.inventoryUpdate.Raise();
        GameState.LockCursor();
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
