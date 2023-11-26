public class RitualCameraFocusable : CameraFocusable {
    public PlayerConstants _playerConstants;

    public override void OnInteraction() {
        base.OnInteraction();
        _playerConstants.raycastDistance = 4f;
    }

    public override void OnEscape() {
        base.OnEscape();
        _playerConstants.raycastDistance = 1f;
    }
}