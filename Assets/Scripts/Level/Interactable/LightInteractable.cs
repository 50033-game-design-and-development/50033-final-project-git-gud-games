using UnityEngine;

public class LightInteractable : MonoBehaviour, IInteractable {
    [SerializeField] protected bool switchedOn = true;
    [SerializeField] protected BoolGameEvent @event;

    public virtual void OnInteraction() {
        ToggleSwitch();
        @event.Raise(switchedOn);
    }

    protected void ToggleSwitch() {
        switchedOn = !switchedOn;
    }

    protected virtual void Start() {
        @event.Raise(switchedOn);
    }
}
