using UnityEngine;

public class LockedCameraFocusable : CameraFocusable {
    [SerializeField] private PlayerConstants playerConstants;

    public override void OnInteraction() {
        base.OnInteraction();
        GameState.raycastDist = 0f;
        GameState.isPuzzleLocked = false;
    }

    public void ForcedEscape() {
        GameState.isPuzzleLocked = true;
        base.OnEscape();
        GameState.raycastDist = playerConstants.raycastDistance;
    }

}
