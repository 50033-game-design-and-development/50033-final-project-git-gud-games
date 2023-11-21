using UnityEngine;

// Adapted from https://fistfullofshrimp.com/unity-drag-things-around/
public class Draggable : MonoBehaviour, IClickable {
    private Plane draggingPlane;
    private Vector3 offset;
    private Camera mainCamera;
    private bool hasOffset;

    // Determine offset of object from camera upon click
    public void OnClick() {
        Debug.Log("LL " + GameState.isPuzzleLocked + " CC " + GameState.isInventoryOpened);
        // Can only be interacted with when focused
        if (GameState.isInteractionAllowed) {
            return;
        }

        draggingPlane = new Plane(Vector3.up, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(GameState.lastPointerDragScreenPos);
        float planeDistance;
        draggingPlane.Raycast(camRay, out planeDistance);
        offset = transform.position - camRay.GetPoint(planeDistance);
        hasOffset = true;
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        // Ensure all conditions are met:
        // 1. Camera is focused on the puzzle
        // 2. Left click has not been released
        // 3. offset value is fresh
        if (GameState.isInteractionAllowed || !GameState.mouseHold || !hasOffset) {
            hasOffset = false;
            return;
        }

        // Drag object around
        Ray camRay = mainCamera.ScreenPointToRay(GameState.lastPointerDragScreenPos);
        float planeDistance;
        draggingPlane.Raycast(camRay, out planeDistance);
        transform.position = camRay.GetPoint(planeDistance) + offset;
    }
}
