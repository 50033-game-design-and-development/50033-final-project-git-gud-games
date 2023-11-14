using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public interface DragCallbacks {
    void OnDragStart(IPointerEvent evt);
}


public class DraggableButton: Button {
    private const string DRAGGING_SLOT_CLASS = "dragging";
    private const int LEFT_MOUSE_PRESS_MASK = 1;
    private readonly DragCallbacks _dragCallbacks;

    public DraggableButton(DragCallbacks dragCallbacks) {
        _dragCallbacks = dragCallbacks;

        RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
        RegisterCallback<PointerMoveEvent>(OnPointerMove);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(IPointerEvent evt) {
        if (evt.pointerId == PointerId.mousePointerId) { 
            AddToClassList(DRAGGING_SLOT_CLASS);
            this.CapturePointer(evt.pointerId);
            _dragCallbacks.OnDragStart(evt);
        }
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
        this.ReleasePointer(evt.pointerId);
    }
}