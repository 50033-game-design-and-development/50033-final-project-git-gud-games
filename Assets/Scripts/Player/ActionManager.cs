using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{

    public UnityEvent<Vector2> moveEvent;
    public UnityEvent<Vector2> mouseMoveEvent;
    public UnityEvent onEscKeyPress;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            moveEvent.Invoke(Vector2.zero);
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseMoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnEscapeKeyPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onEscKeyPress.Invoke();
        }
    }





}
