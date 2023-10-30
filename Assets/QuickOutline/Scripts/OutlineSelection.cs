using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight = null;
    private bool highlighted = false;



    void Update()
    {
        if (highlight != null && !highlighted) 
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Ray points out from the middle of camera viewport 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                highlighted = true;
                highlight = raycastHit.transform;
                Debug.Log("Hit");
                PerformHighlight();
            }
            else
            {
                highlighted = false;
            }
        }
        else
        {
            highlighted = false;
        }
    }

    private void PerformHighlight()
    {
        if (highlight.transform.GetComponent<Outline>() != null)
        {
            highlight.transform.GetComponent<Outline>().enabled = true;
        }
        else 
        {
            AddOutline();
        }
    }

    private void AddOutline()
    {
        Outline outline = highlight.gameObject.AddComponent<Outline>();
        outline.OutlineColor = Color.red;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 10f;
        outline.enabled = true;
    
    }
}
