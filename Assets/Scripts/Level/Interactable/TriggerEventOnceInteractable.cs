using UnityEngine;

public class TriggerEventOnceInteractable : MonoBehaviour, IInteractable {
    private bool hasTriggered;
    [SerializeField] private GameEvent @event;

    public void OnInteraction() {
        if (!hasTriggered) {
            @event.Raise();
            hasTriggered = true;
        }
    }

    public void DisableTrigger() {
        hasTriggered = true;
    }
}
