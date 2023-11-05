using System.Linq;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    private Transform highlight;
    private bool highlighted;
    private PlayerAction playerAction;
    private Vector3 rayOrigin = new(0.5f, 0.5f, 0f);
    private int layerMaskInteractable = 1 << 6;
    private bool allHighlighted = false;
    private GameObject[] interactables;


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
        allHighlighted = true;
        foreach (GameObject gameObject in interactables) EnableOutline(gameObject);
    }

    private void DisableAllOutlines()
    {
        allHighlighted = false;
        foreach (GameObject gameObject in interactables) DisableOutline(gameObject);
        highlight = null;
        highlighted = false;
    }


    /// <summary>
    /// Finds all gameobject in the Interactables layer 
    /// </summary>
    /// <returns>Array of all interactables in the scene</returns>
    private GameObject[] PopulateInteractables()
    {
        // Find all in layer interactables
        GameObject[] interactables = FindObjectsOfType<GameObject>().Where(obj => ((1 << obj.layer) & layerMaskInteractable) > 0).ToArray();
        return interactables;
    }



    void Start()
    {
        interactables = PopulateInteractables();
        playerAction = new PlayerAction();
        playerAction.Enable();

        playerAction.gameplay.Tab.performed += _ => EnableAllOutlines();
        playerAction.gameplay.Tab.canceled += _ => DisableAllOutlines();
        Debug.Log("Start");
    }

    void Update()
    {
        if (allHighlighted) return;

        // Handle cases where player hovers away from object
        if (highlight != null && !highlighted)
            DisableOutline();

        // Ray points out from the middle of camera viewport 
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMaskInteractable))
        {
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                PerformHighlight(raycastHit.transform);
                return;
            }
        }

        highlighted = false;
    }
}
