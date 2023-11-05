using UnityEngine;

public class PlayerRayCast : MonoBehaviour {
    private Transform highlight;
    private bool highlighted;

    private Vector3 rayOrigin = new (0.5f, 0.5f, 0f);

    void Update() {
        // Handle cases where player hovers away from object
        if (highlight != null && !highlighted)
            DisableOutline();

        // Ray points out from the middle of camera viewport 
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
                PerformHighlight(raycastHit.transform);
                return;
            }
        }

        highlighted = false;
    }

    /// <summary>
    /// Performs highlighting of a valid interactable object
    /// </summary>
    private void PerformHighlight(Transform transform) {
        // Hover over same object. Do nothing
        if (highlight != null && highlight.GetInstanceID() == transform.GetInstanceID())
            return;

        // Disable outline on previous object before handling new one
        DisableOutline();

        highlighted = true;
        highlight = transform;

        EnableOutline();
    }

    private void EnableOutline() {
        if (highlight.transform.GetComponent<Outline>() != null) {
            highlight.transform.GetComponent<Outline>().enabled = true;
        } else {
            AddOutline();
        }
    }

    private void AddOutline() {
        Outline outline = highlight.gameObject.AddComponent<Outline>();
        outline.OutlineColor = Color.red;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 10f;
        outline.enabled = true;
    }

    private void DisableOutline() {
        if (highlight == null)
            return;
        highlight.gameObject.GetComponent<Outline>().enabled = false;
        highlight = null;
    }
}
