using UnityEngine;

public class BookInteractable : MonoBehaviour, IInteractable {
    private bool seen;
    private Collider bookCollider;

    public void OnInteraction() {
        if (!seen) {
            Event.L1.seeBook.Raise();
            seen = true;
        }
        bookCollider.enabled = false;
    }

    public void ToggleCollider() {
        bookCollider.enabled = !GameState.inventoryOpened;
    }

    private void Start() {
        bookCollider = GetComponent<Collider>();
    }
}
