using UnityEngine;

public class PaperInteractable : MonoBehaviour, IInteractable {
    [SerializeField] private bool _isPaper1;
    private bool seen;

    public void OnInteraction() {
        if (!_isPaper1) {
            Event.L0.seePaper2.Raise();
        } else if (!seen) {
            Event.L0.seePaper1.Raise();
            seen = true;
        }
    }

    public void SetSeen() {
        seen = true;
    }
}
