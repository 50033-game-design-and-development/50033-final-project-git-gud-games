using System.Collections;
using Unity.VisualScripting;
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
        // Debug.Log("valu4 is" + GameState.selectedInventoryItem.HasValue.ToString());
        if(GameState.selectedInventoryItem.HasValue)
            Debug.Log(GameState.selectedInventoryItem.Value.itemType.ToString());

        Ray ray = Camera.main.ScreenPointToRay(GameState.lastPointerDragScreenPos);
        if (!Physics.Raycast(ray, out RaycastHit raycastHit, LayerMask.GetMask("Interactable"))) return;
        
        // TODO: The event raised is just a temporary measure to get L0P1 working
        // To be replaced with a more robust system that can match where items should go
        switch (GameState.selectedInventoryItem.Value.itemType) {
            case InventoryItems.Key:
                if (raycastHit.transform.gameObject.name == "Doorframe") {
                    TriggerDragInteraction(Event.L0UnlockDoorEvent);
                }
                break;
            case InventoryItems.Paper:
                if (raycastHit.transform.gameObject.name == "Paper1") {
                    TriggerDragInteraction(Event.itemPlaced);
                }
                break;
            default:
                return;
        }

        
    }

    private void TriggerDragInteraction(GameEvent gameEvent) {
        gameEvent.Raise();
        GameState.inventory.Clear();
        Event.onInventoryUpdate.Raise();

    }
    
}
