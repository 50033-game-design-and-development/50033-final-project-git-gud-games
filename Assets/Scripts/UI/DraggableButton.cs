using System.Numerics;
using UnityEngine.UIElements;

public class DraggableButton : Button {
    private const string DRAGGING_SLOT_CLASS = "dragging";
    private const int LEFT_MOUSE_PRESS_MASK = 1;
    private readonly DragCallbacks _dragCallbacks;
    private Vector2? _startDragPosition;

    public DraggableButton(DragCallbacks dragCallbacks) {
        _dragCallbacks = dragCallbacks;

        RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
        RegisterCallback<MouseEnterEvent>(OnMouseEnter, TrickleDown.TrickleDown);
        RegisterCallback<MouseLeaveEvent>(OnMouseLeave, TrickleDown.TrickleDown);
        RegisterCallback<PointerMoveEvent>(OnPointerMove);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private Vector2 GetScreenCoords(IPointerEvent evt) {
        return new Vector2(
            evt.position.x, evt.position.y
        );
    }

    private void OnPointerDown(IPointerEvent evt) {
        if (evt.pointerId != PointerId.mousePointerId) {
            return;
        }

        _startDragPosition = GetScreenCoords(evt);
        AddToClassList(DRAGGING_SLOT_CLASS);
        this.CapturePointer(evt.pointerId);
        _dragCallbacks.OnDragStart(evt);
    }
    
    private void OnPointerMove(IPointerEvent evt) {
        // Check if the left button is pressed
        if (
            evt.pointerId == PointerId.mousePointerId 
            && evt.pressedButtons == LEFT_MOUSE_PRESS_MASK
        ) {
            var coords = GetScreenCoords(evt);
            if (_startDragPosition != null) {
                var displacement = coords - (Vector2) _startDragPosition;
                style.left = displacement.X;
                style.top = displacement.Y;
            }
        }
    }

    private void OnPointerUp(IPointerEvent evt) {
        RemoveFromClassList(DRAGGING_SLOT_CLASS);
        _dragCallbacks.OnDragEnd(evt);

        _startDragPosition = null;
        style.left = 0;
        style.top = 0;
    }
    
    private void OnMouseEnter(MouseEnterEvent evt) {
        _dragCallbacks.OnHoverStart(evt);
    }

    private void OnMouseLeave(MouseLeaveEvent evt) {
        // Code to execute when the mouse stops hovering (leaves) the button
        // For example: Revert color changes, hide a tooltip, etc.
        _dragCallbacks.OnHoverEnd(evt);
    }
}
