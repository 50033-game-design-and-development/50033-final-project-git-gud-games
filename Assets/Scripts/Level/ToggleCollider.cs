using UnityEngine;

public class ToggleCollider : MonoBehaviour {
    [SerializeField] private Collider targetCollider;

    public void Toggle() {
        targetCollider.enabled = !GameState.isInventoryOpened;
    }
}
