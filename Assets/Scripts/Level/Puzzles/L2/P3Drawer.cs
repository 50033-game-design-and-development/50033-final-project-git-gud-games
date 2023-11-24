using UnityEngine;

public class P3Drawer : MonoBehaviour {
    public Animator animator;
    private static readonly int INTERACT = Animator.StringToHash("Interact");

    // called on L2::SolvedP3
    public void OnPuzzleSolved() {
        animator.SetTrigger(INTERACT);
    }
}
