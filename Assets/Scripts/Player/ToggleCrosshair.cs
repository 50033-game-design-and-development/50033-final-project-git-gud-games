using UnityEngine;
using UnityEngine.UI;

public class ToggleCrosshair : MonoBehaviour {
    private Image crosshair;

    public void Toggle() {
        crosshair.enabled = GameState.isInteractionAllowed;
    }

    private void Start() {
        crosshair = GetComponent<Image>();
    }
}
