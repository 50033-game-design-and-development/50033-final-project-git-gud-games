using UnityEngine;

public class AnimationInteractable : MonoBehaviour, IInteractable {
    private Animator animator;

    public void OnInteraction() {
        Debug.Log("Animate " + gameObject.name);
        animator.SetTrigger("Interact");

    }

    private void Start() {
        animator = GetComponent<Animator>();
    }
}