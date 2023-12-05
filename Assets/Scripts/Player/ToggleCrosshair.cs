using UnityEngine;
using UnityEngine.UI;

public class ToggleCrosshair : MonoBehaviour {
    private Image _crosshair;

    private void Update() {
        _crosshair.enabled = GameState.isInteractionAllowed;
    }

    private void Start() {
        _crosshair = GetComponent<Image>();
    }
}
