using UnityEngine;

public class PlayerRayCast : MonoBehaviour {
    public PlayerConstants playerConstants;

    private Transform _highlight;
    private bool _highlighted;
    private PlayerAction _playerAction;
    private readonly Vector3 _rayOrigin = new(0.5f, 0.5f, 0f);
    private int _layerMaskInteractable;


    /// <summary>
    /// Performs highlighting of a valid interactable object
    /// </summary>
    private void PerformHighlight(Transform transform) {
        // Hover over same object. Do nothing
        if (_highlight != null && _highlight.GetInstanceID() == transform.GetInstanceID())
            return;

        // Disable outline on previous object before handling new one
        DisableOutline();

        _highlighted = true;
        _highlight = transform;

        EnableOutline();
    }

    private void EnableOutline() {
        if (_highlight == null)
            return;
        EnableOutline(_highlight.gameObject);
    }

    private void EnableOutline(GameObject obj) {
        if (obj == null)
            return;
        if (obj.GetComponent<Outline>() != null) {
            obj.GetComponent<Outline>().enabled = true;
        } else {
            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineColor = new Color(255, 255, 255, 0.8f);
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
        if (_highlight != null && !_highlighted)
            DisableOutline();

        // Ray points out from the middle of camera viewport 
        Ray ray = Camera.main.ViewportPointToRay(_rayOrigin);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, playerConstants.raycastDistance, _layerMaskInteractable)) {
            PerformHighlight(raycastHit.transform);
            return;
        }

        _highlighted = false;
    }
}