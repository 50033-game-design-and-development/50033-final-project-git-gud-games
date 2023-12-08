using UnityEngine;

public class CinematicBarBehaviour : MonoBehaviour
{
    private Animator _animator;

    // [SerializeField] private string activationBool = "Activated";
    private static readonly int ACTIVATED = Animator.StringToHash("Activated");

    public void ActivateBar() {
        _animator.SetBool(ACTIVATED, true);
    }

    public void DeactivateBar() {
        _animator.SetBool(ACTIVATED, false);
    }

    private void Start() {
        _animator = GetComponent<Animator>();
        DeactivateBar();
    }
}
