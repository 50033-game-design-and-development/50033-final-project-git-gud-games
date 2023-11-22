using UnityEngine;

public class ToggleCollider : MonoBehaviour {
    [SerializeField] private bool canToggle = true;
    [SerializeField] private Collider targetCollider;

    public void Toggle() {
        if (canToggle) {
            Debug.Log(GameState.isPuzzleLocked);
            targetCollider.enabled = !GameState.isPuzzleLocked;
        }
    }

    public void SetCanToggle(bool value) {
        canToggle = value;
    }
}
