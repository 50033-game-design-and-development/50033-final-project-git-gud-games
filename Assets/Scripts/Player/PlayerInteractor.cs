using Cinemachine;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {
    private PlayerAction _playerAction;
    private int _layerMaskInteractable;
    public CinemachineStateDrivenCamera cineMachineCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    private void TriggerInteractions(Vector2 screenPos) {
        // thx linux
        Ray ray = GameState.isInteractionAllowed
                  ? Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))
                  : Camera.main.ScreenPointToRay(screenPos);
       
        if (Physics.Raycast(ray, out RaycastHit raycastHit, GameState.raycastDist, _layerMaskInteractable)) {
            Interact(raycastHit.transform.gameObject);
        }
    }

    private static void Interact(GameObject obj) {
        if(GameState.isInteractionAllowed) {
            foreach (var i in obj.GetComponents<IInteractable>()) {
                i.OnInteraction();
            }
            return;
        }
        // if we only check for isDraggingInventoryItem, we can implement a mechanic where stuff is dragged/dropped
        // onto items in the world itself. Leaving this comment here in case this is ever required (unlikely)
        if (GameState.isPuzzleLocked && GameState.isDraggingInventoryItem) {
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

        // and when inventory is not opened
        // trigger interaction with object on click
        _playerAction.gameplay.MousePress.performed += _ => {
            TriggerInteractions(GameState.lastPointerDragScreenPos);
            GameState.mouseHold = true;
        };

        // trigger drag interaction with object on mouse release
        // and inventory item was previously being dragged
        _playerAction.gameplay.MousePress.canceled += _ => {
            Debug.Log("isDraggingInvItem: "+GameState.isDraggingInventoryItem);
            if (GameState.isDraggingInventoryItem) {
                TriggerInteractions(GameState.lastPointerDragScreenPos);
            }

            GameState.isDraggingInventoryItem = false;
            // GameState.selectedInventoryItem = null;
            // Debug.Log("setting to null");
            GameState.mouseHold = false;
        };

        // open inventory when you press E
        _playerAction.gameplay.InventoryOpen.performed += _ => {
            if(GameState.isCutscenePlaying) return;

            // check if the cineMachine camera is not locked
            // to any interaction objects i.e. it follows the player
            // and don't allow player to freely move cursor if so
            bool adjustCursor = ReferenceEquals(cineMachineCamera.LiveChild, firstPersonCamera);

            // disable the cineMachineCamera if the inventory is opened,
            // otherwise the camera will follow the cursor position
            GameState.ToggleInventory(adjustCursor);
            cineMachineCamera.enabled = GameState.isPuzzleLocked || !GameState.isInventoryOpened;
        };

        // close inventory when you press escape
        _playerAction.gameplay.Escape.performed += _ => {
            if(GameState.isInventoryOpened) {
                GameState.ToggleInventory();
                cineMachineCamera.enabled = GameState.isPuzzleLocked || !GameState.isInventoryOpened;
            } else if(GameState.isInteractionAllowed && !GameState.wasPuzzleLocked) {
                GameState.TogglePause();
            }
            GameState.wasPuzzleLocked = false;
        };
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
}
