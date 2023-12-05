using UnityEngine;

public class L2Revealer : Revealer {
    [SerializeField] private BoolGameEventListener listener;

    public override void OnInteraction() {
        if (listener == null) 
            return;
        listener.enabled = !listener.enabled;
        base.OnInteraction();
    }
}
