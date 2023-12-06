using UnityEngine;

public class ToggleCollider : MonoBehaviour {
    [SerializeField] private Collider _targetCollider;
    [SerializeField] private bool _canToggle = true;
    public bool CanToggle {
        get => _canToggle;
        set => _canToggle = value;
    }

    public void Toggle() {
        if (_canToggle) {
            _targetCollider.enabled = !GameState.isPuzzleLocked;
        }
    }
}
