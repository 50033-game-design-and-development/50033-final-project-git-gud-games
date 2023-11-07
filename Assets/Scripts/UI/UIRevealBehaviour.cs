using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIRevealBehaviour : MonoBehaviour {
    public Transform revealCrosshairPrefab;
    public PostProcessVolume revealVignette;
    public UIConstants uiConstants; 

    private Vector2 _prefabSize;
    private List<RevealUIElement> elements = new List<RevealUIElement>();

    private bool isRevealed = false;
    private bool isResetting = false;


    /// <summary>
    /// Called on a Reveal UI Event to reveal a crosshair at the given coordinates, if not rendered
    /// Also refreshes the reveal cooldown.
    /// </summary>
    /// <param name="coords">World coordinates to render the crosshair on</param>
    public void OnReveal(Vector3 coords) {

        isRevealed = true;
        revealVignette.weight = 1f;

        if (IsCoordInList(coords)) return;

        RevealUIElement element = new RevealUIElement(
            coords,
            Instantiate(revealCrosshairPrefab, coords, Quaternion.identity)
        );

        element.crosshair.SetParent(transform);
        while (isResetting) ;
        elements.Add(element);
    }


    /// <summary>
    /// Called on an Unreveal Event to fade out the reveal overlay and destroy all crosshairs
    /// </summary>
    public void OnUnreveal() {
        isRevealed = false;
        StartCoroutine(DelayedUnreveal());
    }


    /// <summary>
    /// Coroutine to fade out the reveal overlay and destroy all crosshairs 
    /// within a delay time specified in UIConstants SO
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DelayedUnreveal() {
        float timeDecrement = uiConstants.RevealDestroyDelay / 40f;
        for (float alpha = 1f; alpha >= -0.05f; alpha -= timeDecrement)
        {
            if (isRevealed) {
                revealVignette.weight = 1f;
                yield break;
            }

            revealVignette.weight = alpha;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        // Blocks execution of OnReveal() until elements are all destroyed
        if (isRevealed) yield break;
        isResetting = true;


        // After the reveal overlay has faded out, destroy all the crosshairs
        for (int i = 0; i < elements.Count; i++) {
            Destroy(elements[i].crosshair.gameObject);
        }

        elements.Clear();
        isResetting = false;
    }


    /// <summary>
    /// Check if any RevealUIElement already has the world coords passed in
    /// </summary>
    /// <param name="coords">World coordinates to check</param>
    /// <returns>True if any RevealUIElement has the coordinate specified</returns>
    private bool IsCoordInList(Vector3 coords) {
        for (int i = 0; i < elements.Count; i++) {
            if (elements[i].coords == coords) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Adjust the transform of the crosshair to match the world coordinates.
    /// Called on Update() to update the position and scale to accommodate
    /// to changes in screen size and camera transform
    /// </summary>
    /// <param name="crosshair">Crosshair transform</param>
    /// <param name="coords">World coordinates of object</param>
    private void AdjustCrosshairTransform(Transform crosshair, Vector3 coords) {
        Vector3 screenCoords = Camera.main.WorldToScreenPoint(coords);
            
        // Scale with width
        float scale = (Screen.width * uiConstants.RevealScale) / _prefabSize.x;

        crosshair.localScale = new Vector3(scale, scale, 1f);
        crosshair.position = screenCoords;
        crosshair.gameObject.SetActive(screenCoords.z > 0);
    }

   
    private void Update() {
        for (int i = 0; i < elements.Count; i++) {
            RevealUIElement element = elements[i];
            AdjustCrosshairTransform(element.crosshair, element.coords);
        }
    }

    private void Start() {
        _prefabSize = revealCrosshairPrefab.GetComponent<RectTransform>().sizeDelta;
        revealVignette.weight = 0;
        revealVignette.gameObject.SetActive(true);
    }
    
}


/// <summary>
/// Data class to store the world coordinates at which to render a crosshair
/// </summary>
class RevealUIElement {
    public Vector3 coords;
    public Transform crosshair;

    public RevealUIElement(Vector3 coords, Transform crosshair) {
        if (coords == null) throw new System.ArgumentNullException("coords");
        this.coords = coords;
        this.crosshair = crosshair;
    }
}
