using Cinemachine;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {
    private PlayerAction _playerAction;
    private int _layerMaskInteractable;
    
    public GameEvent onInventoryUpdate;
    public CinemachineStateDrivenCamera cineMachineCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public PlayerConstants playerConstants;

    private void TriggerInteractions(Vector2 screenPos) {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, playerConstants.raycastDistance, _layerMaskInteractable)) {
            Interact(raycastHit.transform.gameObject);
        }
    }

    private static void Interact(GameObject obj) {
        foreach (var i in obj.GetComponents<IInteractable>()) {
            i.OnInteraction();
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
            if (!GameState.inventoryOpened) {
                TriggerInteractions(GameState.lastPointerDragScreenPos);
            }
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
        
        // open inventory when you press E
        _playerAction.gameplay.InventoryOpen.performed += _ => {
            Debug.Log("TOGGLE");
            GameState.ToggleInventory();

            // check if the cinemachine camera is not locked
            // to any interaction objects i.e. it follows the player
            if (cineMachineCamera.LiveChild == firstPersonCamera) {
                if (GameState.inventoryOpened) {
                    // disable the cineMachineCamera if the inventory
                    // is opened, otherwise the camera will follow
                    // the cursor position
                    cineMachineCamera.enabled = false;
                } else {
                    cineMachineCamera.enabled = true;
                }
            }

            onInventoryUpdate.Raise();
        };
        
        // close inventory when you press escape
        _playerAction.gameplay.Escape.performed += _ => {
            // GameState.HideInventory();
            onInventoryUpdate.Raise();
        };
    }
}