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
        if (GameState.inventoryOpened) {
            // dont use the camera to move the CineMachine
            // to an interactable object if inventory is already opened
            return;
        }
        
        cinemachineAnimator.Play(startStateName);

        // don't open inventory if it's empty
        if (GameState.inventory.Count == 0) {
            return;
        }

        GameState.inventoryOpened = true;
        Event.Global.inventoryUpdate.Raise();
    }
    
    private void OnEscape() {
        cinemachineAnimator.Play(endStateName);
        GameState.inventoryOpened = false;
        Event.Global.inventoryUpdate.Raise();
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
