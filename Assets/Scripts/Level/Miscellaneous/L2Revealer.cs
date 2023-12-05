using UnityEngine;

public class L2Revealer : Revealer {
    [SerializeField] private BoolGameEventListener listener;

    public override void OnInteraction() {
        listener.enabled = !listener.enabled;
        base.OnInteraction();
    }
}
