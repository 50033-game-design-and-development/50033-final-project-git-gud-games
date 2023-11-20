using UnityEngine;

public class EventInvocable : MonoBehaviour, IInteractable {
    [SerializeField] private GameEvent @event;

    public void OnInteraction() {
        @event.Raise();
    }

}
