using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockInteractable : MonoBehaviour {
    public Animator cinemachineAnimator;
    
    /// <summary>
    /// Called when player clicks on first puzzle paper.
    /// </summary>
    public virtual void OnInteraction() {
        cinemachineAnimator.Play("L0 Door Lock");
        // TODO - open inventory, freeze player movement (player is auto frozen though)
    }
    
    /// <summary>
    /// Called when player exits puzzle scene.
    /// </summary>
    public void OnEscape() {
        cinemachineAnimator.Play("L0 First Person");
    }
    
    private void Start()
    {
        
    }
    
    private void Update() {
        // For testing purposes
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            print("alpha3 pressed");
            OnInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            OnEscape();
            
        }
    }
}
