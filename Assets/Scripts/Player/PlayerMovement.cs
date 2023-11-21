using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public PlayerConstants playerConstants;

    /// <summary>
    /// The vector the player should move in based on player input
    /// </summary>
    private Vector3 moveVector = Vector2.zero;

    private CharacterController controller;

    private PlayerAction playerAction;


    /// <summary>
    /// Called by the ActionManager when the player moves (WASD or Arrow keys).
    /// </summary>
    /// <param name="direction"></param>
    private void OnMove(Vector2 direction) {
        moveVector.x = direction.x;
        moveVector.z = direction.y;
    }

    /// <summary>
    /// Calculate the direction the player should move in based on the camera's orientation.
    /// </summary>
    private Vector3 CalculateMoveDirection() {
        Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

        Vector3 moveDirection = cameraForward.normalized * moveVector.z
                                + cameraRight.normalized * moveVector.x;
        
        return new Vector3(moveDirection.x, playerConstants.gravity, moveDirection.z);
    }

    private void Move() {
        // don't move when the inventory is open
        if (GameState.isInventoryOpened) {
            controller.Move(Vector3.zero);
            return;
        }
        
        Vector3 moveDirection = CalculateMoveDirection();
        controller.Move(moveDirection * playerConstants.moveSpeed * Time.deltaTime);
    }

    private void Start() {
        controller = GetComponent<CharacterController>();

        playerAction = new PlayerAction();
        playerAction.Enable();

        playerAction.gameplay.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        playerAction.gameplay.Move.canceled += ctx => OnMove(ctx.ReadValue<Vector2>());
    }

    private void Update() {
        Move();
    }
}
