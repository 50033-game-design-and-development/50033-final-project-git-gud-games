using UnityEngine;

public class LockedCameraFocusable : CameraFocusable {
    [SerializeField] private PlayerConstants playerConstants;
    private bool _cachedisPuzzleLocked;


    public override void OnInteraction() {
        base.OnInteraction();
        GameState.raycastDist = 0f;
        _cachedisPuzzleLocked = GameState.isPuzzleLocked;
        GameState.isPuzzleLocked = false;
        GameState.isCutscenePlaying = true;
    }

    public void ForcedEscape() {
        GameState.isPuzzleLocked = _cachedisPuzzleLocked;
        GameState.isCutscenePlaying = false;
        base.OnEscape();
        GameState.raycastDist = playerConstants.raycastDistance;
    }
}
