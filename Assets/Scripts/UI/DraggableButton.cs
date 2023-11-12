using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public interface DragCallbacks {
    void OnDragStart(PointerDownEvent evt);
}


public class DraggableButton: Button {
    private const string DRAGGING_SLOT_CLASS = "dragging";
    private readonly DragCallbacks _dragCallbacks;

    public DraggableButton(DragCallbacks dragCallbacks) {
        _dragCallbacks = dragCallbacks;

        RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
        RegisterCallback<PointerMoveEvent>(OnPointerMove);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt) {
        if (evt.pointerId == PointerId.mousePointerId) { 
            GameState.IsDraggingInventoryItem = true;
            AddToClassList(DRAGGING_SLOT_CLASS);
            this.CapturePointer(evt.pointerId);
            _dragCallbacks.OnDragStart(evt);
        }
    }
    
    private void OnPointerMove(PointerMoveEvent evt) {
        // Check if the left button is pressed
        if (
            evt.pointerId == PointerId.mousePointerId 
            && evt.pressedButtons == 1
        ) {
            // TODO: maybe show the inventory item moving on cursor drag
        }
    }
    
    private void OnPointerUp(PointerUpEvent evt) {
        RemoveFromClassList(DRAGGING_SLOT_CLASS);
        this.ReleasePointer(evt.pointerId);
    }
}