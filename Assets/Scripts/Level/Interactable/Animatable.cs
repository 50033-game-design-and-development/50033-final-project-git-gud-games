using UnityEngine;

public class Animatable : MonoBehaviour, IInteractable {
    private Animator _animator;
    private static readonly int INTERACT = Animator.StringToHash("Interact");

    public void OnInteraction() {
        Debug.Log("Animate " + gameObject.name);
        _animator.SetTrigger(INTERACT);
    }

    private void Start() {
        _animator = GetComponent<Animator>();
    }
}