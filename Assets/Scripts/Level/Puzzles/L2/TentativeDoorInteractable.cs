using UnityEngine;

public class TentativeDoorInteractable: MonoBehaviour, IInteractable {
    public Animator animator;
    private static readonly int INTERACT = Animator.StringToHash("Interact");
    
    public void OnInteraction() {
        Debug.Log("OPEN DOOR");
        animator.SetTrigger(INTERACT);
    }
}

