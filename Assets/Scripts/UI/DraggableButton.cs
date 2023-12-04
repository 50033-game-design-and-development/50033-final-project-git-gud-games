using UnityEngine.UIElements;

public class DraggableButton : Button {
    private const string DRAGGING_SLOT_CLASS = "dragging";
    private const int LEFT_MOUSE_PRESS_MASK = 1;
    private readonly DragCallbacks _dragCallbacks;

    public DraggableButton(DragCallbacks dragCallbacks) {
        _dragCallbacks = dragCallbacks;

        RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
        RegisterCallback<MouseEnterEvent>(OnMouseEnter, TrickleDown.TrickleDown);
        RegisterCallback<MouseLeaveEvent>(OnMouseLeave, TrickleDown.TrickleDown);
        RegisterCallback<PointerMoveEvent>(OnPointerMove);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(IPointerEvent evt) {
        if (evt.pointerId != PointerId.mousePointerId) {
            return;
        }

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
            // TODO: maybe show the inventory item moving on cursor drag
        }
    }

    private void OnPointerUp(IPointerEvent evt) {
        RemoveFromClassList(DRAGGING_SLOT_CLASS);
        _dragCallbacks.OnDragEnd(evt);

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
