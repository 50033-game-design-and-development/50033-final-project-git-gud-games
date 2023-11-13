using UnityEngine;

public class PlayerInteractor : MonoBehaviour {
    private PlayerAction _playerAction;
    private int _layerMaskInteractable;
    
    public PlayerConstants playerConstants;

    void OnClick(Vector2 screenPos) {
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
            GameState.LastPointerDragScreenPos = ctx.ReadValue<Vector2>();
            OnClick(GameState.LastPointerDragScreenPos);
        };
        // trigger drag interaction with object on mouse release
        _playerAction.gameplay.MousePress.canceled += ctx => {
            if (GameState.IsDraggingInventoryItem) {
                OnClick(GameState.LastPointerDragScreenPos);
            }
            
            GameState.IsDraggingInventoryItem = false;
            GameState.SelectedInventoryItem = null;
        };
    }
}