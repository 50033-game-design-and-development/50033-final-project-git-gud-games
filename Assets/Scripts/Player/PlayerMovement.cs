using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{

    public PlayerConstants playerConstants;

    /// <summary>
    /// The vector the player should move in based on player input
    /// </summary>
    private Vector3 moveVector = Vector2.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        controller.Move(moveDirection * playerConstants.moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Called by the ActionManager when the player moves (WASD or Arrow keys).
    /// </summary>
    /// <param name="direction"></param>
    public void OnMoveInput(Vector2 direction)
    {
        moveVector.x = direction.x;
        moveVector.z = direction.y;
    }

    /// <summary>
    /// Calculate the direction the player should move in based on the camera's orientation.
    /// </summary>
    public Vector3 CalculateMoveDirection()
    {
        Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

        Vector3 moveDirection = cameraForward.normalized * moveVector.z 
                                + cameraRight.normalized * moveVector.x;

        return new Vector3(moveDirection.x, 0, moveDirection.z);
        
    }

    
    
}
