using UnityEngine;

public class PlayerRayCast : MonoBehaviour {
    private static Color OUTLINE_COLOR = new Color(255, 255, 255, 0.8f);
    public PlayerConstants playerConstants;

    private Transform _highlight;
    private bool _highlighted;
    private PlayerAction _playerAction;
    private readonly Vector3 _rayOrigin = new(0.5f, 0.5f, 0f);
    private int _layerMaskInteractable;

    private void PerformHighlight(Transform transform) {
        PerformHighlight(transform, OUTLINE_COLOR);
    }

    /// <summary>
    /// Performs highlighting of a valid interactable object
    /// </summary>
    private void PerformHighlight(Transform transform, Color highlightColor) {
        // Hover over same object. Do nothing
        if (_highlight != null && _highlight.GetInstanceID() == transform.GetInstanceID())
            return;

        // Disable outline on previous object before handling new one
        DisableOutline();

        _highlighted = true;
        _highlight = transform;

        EnableOutline(highlightColor);
    }
    
    private void EnableOutline() {
        if (_highlight == null)
            return;
        EnableOutline(_highlight.gameObject, OUTLINE_COLOR);
    }
    
    private void EnableOutline(Color highlightColor) {
        EnableOutline(_highlight.gameObject, highlightColor);       
    }

    private void EnableOutline(GameObject obj, Color highlightColor) {
        if (obj == null)
            return;
        if (obj.GetComponent<Outline>() != null) {
            obj.GetComponent<Outline>().OutlineColor = highlightColor;
            obj.GetComponent<Outline>().enabled = true;
        } else {
            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineColor = highlightColor;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineWidth = 5f;
            outline.enabled = true;
        }
    }

    private void DisableOutline() {
        if (_highlight == null)
            return;
        DisableOutline(_highlight.gameObject);
        _highlight = null;
    }

    private void DisableOutline(GameObject obj) {
        obj.GetComponent<Outline>().enabled = false;
    }

    private void EnableAllOutlines() {
        Event.revealAll.Raise();
    }

    private void DisableAllOutlines() {
        Event.hideAll.Raise();
        _highlight = null;
        _highlighted = false;
    }

    private void Start() {
        _layerMaskInteractable = LayerMask.GetMask("Interactable");

        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.RevealItems.performed += _ => EnableAllOutlines();
        _playerAction.gameplay.RevealItems.canceled += _ => DisableAllOutlines();
    }

    private void Update() {
        // Handle cases where player hovers away from object
        if (_highlight != null && !_highlighted) {
            DisableOutline();
        }

        if (GameState.InventoryOpened) {
            if (!GameState.IsDraggingInventoryItem) {
                _highlighted = false;
                DisableOutline();
                return;
            }
            
            // Ray points out from cursor position in camera viewport
            Ray ray = Camera.main.ScreenPointToRay(
                GameState.LastPointerDragScreenPos
            );
            
            if (Physics.Raycast(
                ray, out RaycastHit raycastHit, playerConstants.raycastDistance,
                _layerMaskInteractable
            )) {
                PerformHighlight(raycastHit.transform);
                return;
            }
        } else {
            // Ray points out from the middle of camera viewport 
            Ray ray = Camera.main.ViewportPointToRay(_rayOrigin);
            if (Physics.Raycast(
                ray, out RaycastHit raycastHit, playerConstants.raycastDistance,
                _layerMaskInteractable
            )) {
                PerformHighlight(raycastHit.transform);
                return;
            }
        }
        
        
        _highlighted = false;
        DisableOutline();
    }
}