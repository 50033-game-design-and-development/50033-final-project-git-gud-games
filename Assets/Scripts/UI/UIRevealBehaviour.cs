using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UIRevealBehaviour : MonoBehaviour {
    public Transform revealCrosshairPrefab;
    public UIConstants uiConstants; 

    private List<RevealUIElement> elements = new List<RevealUIElement>();


    public void OnReveal(Vector3 coords) {
        RevealUIElement element = new RevealUIElement(
            coords,
            Instantiate(revealCrosshairPrefab, coords, Quaternion.identity)
        );
        element.crosshair.SetParent(transform);
        elements.Add(element);
    }

    public void OnUnreveal() {
        StartCoroutine(DelayedDestroy());
    }

    // Update is called once per frame
    private void Update() {
        for (int i = 0; i < elements.Count; i++) {
            RevealUIElement element = elements[i];
            Vector3 screenCoords = Camera.main.WorldToScreenPoint(element.coords);
            
            // Set size to 15% of screen
            float size = Mathf.Min(Screen.width, Screen.height) * 0.15f;
            element.crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);

            element.crosshair.position = screenCoords;
            element.crosshair.gameObject.SetActive(screenCoords.z > 0);
        }
    }

    private IEnumerator DelayedDestroy() {
        yield return new WaitForSeconds(uiConstants.RevealDestroyDelay);
        for (int i = 0; i < elements.Count; i++) {
            Destroy(elements[i].crosshair.gameObject);
        }

        elements.Clear();
    }
    
}

class RevealUIElement {
    public Vector3 coords;
    public Transform crosshair;

    public RevealUIElement(Vector3 coords, Transform crosshair) {
        this.coords = coords;
        this.crosshair = crosshair;
    }
}
