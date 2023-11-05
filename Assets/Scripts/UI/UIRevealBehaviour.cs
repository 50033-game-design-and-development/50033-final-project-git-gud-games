using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRevealBehaviour : MonoBehaviour
{
    public Transform revealCrosshairPrefab;

    private List<RevealUIElement> elements = new List<RevealUIElement>();


    public void OnReveal(Vector3 coords)
    {
        RevealUIElement element = new RevealUIElement(coords, Instantiate(revealCrosshairPrefab, coords, Quaternion.identity));
        element.crosshair.SetParent(transform);
        elements.Add(element);
    }

    public void OnUnreveal()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            Destroy(elements[i].crosshair.gameObject);
        }
        elements.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            RevealUIElement element = elements[i];
            Vector3 screenCoords = Camera.main.WorldToScreenPoint(element.coords);
            element.crosshair.position = screenCoords;
            element.crosshair.gameObject.SetActive(screenCoords.z > 0);
        }
    }

}

class RevealUIElement
{
    public Vector3 coords;
    public Transform crosshair;

    public RevealUIElement(Vector3 coords, Transform crosshair)
    {
        this.coords = coords;
        this.crosshair = crosshair;
    }
}
