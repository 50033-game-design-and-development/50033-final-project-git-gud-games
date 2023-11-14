using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {
    public PlayerConstants playerConstants;

    float _rotationY;
    float _rotationX;

    public GameEvent onInventoryUpdate;

    private PlayerAction _playerAction;

    /// <summary>
    /// Called by the ActionManager when the mouse is moved.
    /// </summary>
    /// <param name="mouseDelta">Mouse move delta</param>
    private void OnMouseMove(Vector2 mouseDelta) {
        // don't adjust camera based on mouse movement if inventory is opened
        if (GameState.inventoryOpened) {
            return;
        }

        _rotationX = transform.localEulerAngles.y + mouseDelta.x * playerConstants.mouseSensitivityX;
        _rotationY += mouseDelta.y * playerConstants.mouseSensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, playerConstants.viewMinimumY, playerConstants.viewMaximumY);
    }

    /// <summary>
    /// Toggles the cursor lock state between locked and confined.
    /// A locked cursor is positioned in the center of the view and cannot be moved.
    /// The cursor is invisible in locked state, regardless of the value of Cursor.visible.
    /// </summary>
    private void ToggleCursorLockState() {
        Cursor.lockState = (
            Cursor.lockState == CursorLockMode.Locked
            ? CursorLockMode.Confined
            : CursorLockMode.Locked
        );
    }

    private void Start() {
        _rotationX = transform.localEulerAngles.y;
        _rotationY = -transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;

        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.MouseMove.performed += ctx => OnMouseMove(ctx.ReadValue<Vector2>());
        _playerAction.gameplay.Escape.performed += _ => ToggleCursorLockState();

        // open inventory when you press E
        _playerAction.gameplay.InventoryOpen.performed += _ => {
            GameState.ToggleInventory();
            Debug.Log("TOGGLE");
            onInventoryUpdate.Raise();
        };
        // close inventory when you press escape
        _playerAction.gameplay.Escape.performed += _ => {
            // GameState.HideInventory();
            onInventoryUpdate.Raise();
        };
    }

    private void Update() {
        transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0);
    }
}