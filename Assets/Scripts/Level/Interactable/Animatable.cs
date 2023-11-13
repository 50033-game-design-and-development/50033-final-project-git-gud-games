using UnityEngine;

public class Animatable : MonoBehaviour, IInteractable {
    private Animator _animator;
    private static readonly int INTERACT = Animator.StringToHash("Interact");

    public void OnInteraction() {
        Debug.Log("Animate " + gameObject.name);
        _animator.SetTrigger(INTERACT);

        // Used to test grabbing the paper
        // TODO: To be removed after a better solution is found
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Start() {
        _animator = GetComponent<Animator>();
    }
}
