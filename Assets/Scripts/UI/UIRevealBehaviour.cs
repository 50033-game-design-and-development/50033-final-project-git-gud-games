using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UIRevealBehaviour : MonoBehaviour {
    public Transform revealCrosshairPrefab;
    // public Canvas revealOverlay;
    public UIConstants uiConstants; 

    private Vector2 _prefabSize;
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

    private IEnumerator DelayedDestroy() {
        yield return new WaitForSeconds(uiConstants.RevealDestroyDelay);
        for (int i = 0; i < elements.Count; i++) {
            Destroy(elements[i].crosshair.gameObject);
        }

        elements.Clear();
    }

    // Update is called once per frame
    private void Update() {
        for (int i = 0; i < elements.Count; i++) {
            RevealUIElement element = elements[i];
            Vector3 screenCoords = Camera.main.WorldToScreenPoint(element.coords);
            
            // Scale with width
            float scale = (Screen.width * uiConstants.RevealScale) / _prefabSize.x;

            element.crosshair.localScale = new Vector3(scale, scale, 1f);
            element.crosshair.position = screenCoords;
            element.crosshair.gameObject.SetActive(screenCoords.z > 0);
        }
    }

    private void Start() {
        _prefabSize = revealCrosshairPrefab.GetComponent<RectTransform>().sizeDelta;
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
