using UnityEngine;

public class PlayerInteractor : MonoBehaviour {
    private PlayerAction _playerAction;
    private int _layerMaskInteractable;
    
    public PlayerConstants playerConstants;

    private void TriggerInteractions(Vector2 screenPos) {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, playerConstants.raycastDistance, _layerMaskInteractable)) {
            Interact(raycastHit.transform.gameObject);
        }
    }

    private static void Interact(GameObject obj) {
        if (!GameState.inventoryOpened) {
            foreach (var i in obj.GetComponents<IInteractable>()) {
                i.OnInteraction();
            }
            return;
        }
        if (GameState.isDraggingInventoryItem) {
            foreach (var i in obj.GetComponents<IDragDroppable>()) {
                i.OnDragDrop();
            }
            return;
        }
        foreach (var i in obj.GetComponents<IClickable>()) {
            i.OnClick();
        }
    }
    
    private void Start() {
        _layerMaskInteractable = LayerMask.GetMask("Interactable");

        _playerAction = new PlayerAction();
        _playerAction.Enable();
        _playerAction.gameplay.MousePos.performed += ctx => {
            GameState.lastPointerDragScreenPos = ctx.ReadValue<Vector2>();
        };

        // trigger interaction with object on click
        // and when inventory is not opened
        _playerAction.gameplay.MousePress.performed += ctx => {
            TriggerInteractions(GameState.lastPointerDragScreenPos);
        };

        // trigger drag interaction with object on mouse release
        // and inventory item was previously being dragged
        _playerAction.gameplay.MousePress.canceled += ctx => {
            if (GameState.isDraggingInventoryItem) {
                TriggerInteractions(GameState.lastPointerDragScreenPos);
            }
            
            GameState.isDraggingInventoryItem = false;
            GameState.selectedInventoryItem = null;
        };
    }
}
