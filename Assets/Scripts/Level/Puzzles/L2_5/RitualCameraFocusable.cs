using UnityEngine;

public class RitualCameraFocusable : CameraFocusable {
    [SerializeField] private PlayerConstants playerConstants;

    public override void OnInteraction() {
        base.OnInteraction();
        GameState.raycastDist = 4f;
    }

    public override void OnEscape() {
        base.OnEscape();
        GameState.raycastDist = 1.75f;
    }
}
