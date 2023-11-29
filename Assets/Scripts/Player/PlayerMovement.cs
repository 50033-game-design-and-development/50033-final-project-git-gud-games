using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public PlayerConstants playerConstants;

    /// <summary>
    /// The vector the player should move in based on player input
    /// </summary>
    private Vector3 _moveVector = Vector2.zero;

    private CharacterController _controller;

    private PlayerAction _playerAction;


    /// <summary>
    /// Called by the ActionManager when the player moves (WASD or Arrow keys).
    /// </summary>
    /// <param name="direction"></param>
    private void OnMove(Vector2 direction) {
        _moveVector.x = direction.x;
        _moveVector.z = direction.y;
    }

    /// <summary>
    /// Calculate the direction the player should move in based on the camera's orientation.
    /// </summary>
    private Vector3 CalculateMoveDirection() {
        Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

        Vector3 moveDirection = cameraForward.normalized * _moveVector.z
                                + cameraRight.normalized * _moveVector.x;
        
        return new Vector3(moveDirection.x, playerConstants.gravity, moveDirection.z);
    }

    private void Move() {
        Vector3 moveDirection = CalculateMoveDirection();
        _controller.Move(moveDirection * playerConstants.moveSpeed * Time.deltaTime);
    }

    private void Start() {
        _controller = GetComponent<CharacterController>();

        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        _playerAction.gameplay.Move.canceled += ctx => OnMove(ctx.ReadValue<Vector2>());
    }

    private void Update() {
        if (GameState.isInteractionAllowed) {
            Move();
        } else {
            _controller.Move(Vector3.zero);
        }
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
}
