using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {
    public PlayerConstants playerConstants;

    float rotationY;
    float rotationX;

    private PlayerAction playerAction;

    /// <summary>
    /// Called by the ActionManager when the mouse is moved.
    /// </summary>
    /// <param name="mouseDelta">Mouse move delta</param>
    private void OnMouseMove(Vector2 mouseDelta) {
        rotationX = transform.localEulerAngles.y + mouseDelta.x * playerConstants.mouseSensitivityX;
        rotationY += mouseDelta.y * playerConstants.mouseSensitivityY;
        rotationY = Mathf.Clamp(rotationY, playerConstants.viewMinimumY, playerConstants.viewMaximumY);
    }

    /// <summary>
    /// Toggles the cursor lock state between locked and confined.
    /// A locked cursor is positioned in the center of the view and cannot be moved.
    /// The cursor is invisible in locked state, regardless of the value of Cursor.visible.
    /// </summary>
    private void ToggleCursorLockState() {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start() {
        rotationX = transform.localEulerAngles.y;
        rotationY = -transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;

        playerAction = new PlayerAction();
        playerAction.Enable();

        playerAction.gameplay.MouseMove.performed += ctx => OnMouseMove(ctx.ReadValue<Vector2>());
        playerAction.gameplay.Escape.performed += _ => ToggleCursorLockState();
        playerAction.gameplay.InventoryOpen.performed += _ => GameState.ConfineCursor();
    }

    private void Update() {
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
