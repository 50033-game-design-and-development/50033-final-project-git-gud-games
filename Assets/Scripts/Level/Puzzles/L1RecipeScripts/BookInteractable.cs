using UnityEngine;

public class BookInteractable : MonoBehaviour, IInteractable {
    private bool seen;

    public void OnInteraction() {
        if (!seen) {
            Event.L1.seeBook.Raise();
            seen = true;
        }
    }
}
