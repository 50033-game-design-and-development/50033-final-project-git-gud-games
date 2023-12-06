using UnityEngine;

public class EventInvocable : MonoBehaviour, IInteractable {
    [SerializeField] private bool canTrigger = true;
    [SerializeField] private GameEvent @event;

    public void OnInteraction() {
        if (canTrigger) {
            @event.Raise();
            canTrigger = false;
        }
    }

    public void DisableTrigger() {
        canTrigger = false;
    }

    public void EnableTrigger() {
        canTrigger = true;
    }
}
