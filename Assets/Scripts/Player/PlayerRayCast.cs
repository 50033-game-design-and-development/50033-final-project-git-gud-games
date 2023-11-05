using System.Linq;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public PlayerConstants playerConstants;
    public GameEvent onRevealAll;
    public GameEvent onUnrevealAll;

    private Transform highlight;
    private bool highlighted;
    private PlayerAction playerAction;
    private Vector3 rayOrigin = new(0.5f, 0.5f, 0f);
    private int layerMaskInteractable = 1 << 6;


    /// <summary>
    /// Performs highlighting of a valid interactable object
    /// </summary>
    private void PerformHighlight(Transform transform)
    {
        // Hover over same object. Do nothing
        if (highlight != null && highlight.GetInstanceID() == transform.GetInstanceID())
            return;

        // Disable outline on previous object before handling new one
        DisableOutline();

        highlighted = true;
        highlight = transform;

        EnableOutline();
    }

    private void EnableOutline()
    {
        if (highlight == null) return;
        EnableOutline(highlight.gameObject);
    }

    private void EnableOutline(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<Outline>() != null)
        {
            obj.GetComponent<Outline>().enabled = true;
        }
        else
        {
            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineColor = new Color(255, 255, 255, 0.8f);
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineWidth = 5f;
            outline.enabled = true;
        }
    }

    private void DisableOutline()
    {
        if (highlight == null) return;
        DisableOutline(highlight.gameObject);
        highlight = null;
    }

    private void DisableOutline(GameObject obj)
    {
        obj.GetComponent<Outline>().enabled = false;
    }

    private void EnableAllOutlines()
    {
        onRevealAll.Raise();
    }

    private void DisableAllOutlines()
    {
        onUnrevealAll.Raise();
        highlight = null;
        highlighted = false;
    }

    void Start()
    {
        // interactables = PopulateInteractables();
        playerAction = new PlayerAction();
        playerAction.Enable();

        playerAction.gameplay.Tab.performed += _ => EnableAllOutlines();
        playerAction.gameplay.Tab.canceled += _ => DisableAllOutlines();
    }

    void Update()
    {
        // Handle cases where player hovers away from object
        if (highlight != null && !highlighted)
            DisableOutline();

        // Ray points out from the middle of camera viewport 
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMaskInteractable))
        {
            if (raycastHit.distance <= playerConstants.raycastDistance
                && raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                PerformHighlight(raycastHit.transform);
                return;
            }
        }

        highlighted = false;
    }
}
