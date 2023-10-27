using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public PlayerConstants playerConstants;
    float rotationY = 0f;
    float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rotationX = transform.localEulerAngles.y;
        rotationY = -transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    /// <summary>
    /// Called by the ActionManager when the mouse is moved.
    /// </summary>
    /// <param name="mouseDelta">Mouse move delta</param>
    public void OnMouseMove(Vector2 mouseDelta)
    {
        rotationX = transform.localEulerAngles.y + mouseDelta.x * playerConstants.mouseSensitivityX;
        rotationY += mouseDelta.y * playerConstants.mouseSensitivityY; 
        rotationY = Mathf.Clamp(rotationY, playerConstants.viewMinimumY, playerConstants.viewMaximumY);
    }

    /// <summary>
    /// Toggles the cursor lock state between locked and confined.
    /// A locked cursor is positioned in the center of the view and cannot be moved. 
    /// The cursor is invisible in locked state, regardless of the value of Cursor.visible.
    /// </summary>
    public void ToggleCursorLockState()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
