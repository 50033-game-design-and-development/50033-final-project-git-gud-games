using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIRevealBehaviour : MonoBehaviour {
    public Transform revealCrosshairPrefab;
    public PostProcessVolume revealVignette;
    public UIConstants uiConstants;

    private Vector2 _prefabSize;
    private List<RevealUIElement> _elements = new();

    private bool _isRevealed;


    /// <summary>
    /// Called on a Reveal UI Event to reveal a crosshair at the given coordinates, if not rendered
    /// Also refreshes the reveal cooldown.
    /// </summary>
    /// <param name="coords">World coordinates to render the crosshair on</param>
    public void OnReveal(Vector3 coords) {
        _isRevealed = true;
        revealVignette.weight = 1f;

        if (IsCoordInList(coords))
            return;

        RevealUIElement element = new RevealUIElement(
            coords,
            Instantiate(revealCrosshairPrefab, coords, Quaternion.identity)
        );

        element.crosshair.SetParent(transform);
        _elements.Add(element);
    }


    /// <summary>
    /// Called on a Hide Event to fade out the reveal overlay and destroy all crosshairs
    /// </summary>
    public void OnHide() {
        _isRevealed = false;
        StartCoroutine(DelayedHide());
    }


    /// <summary>
    /// Coroutine to fade out the reveal overlay and destroy all crosshairs 
    /// within a delay time specified in UIConstants SO
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DelayedHide() {
        float timeDecrement = uiConstants.RevealDestroyDelay / 40f;
        for (float alpha = 1f; alpha >= -0.05f; alpha -= timeDecrement) {
            if (_isRevealed) {
                revealVignette.weight = 1f;
                yield break;
            }

            revealVignette.weight = alpha;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        // Blocks execution of OnReveal() until elements are all destroyed
        if (_isRevealed)
            yield break;


        // After the reveal overlay has faded out, destroy all the crosshairs
        foreach (var t in _elements) {
            Destroy(t.crosshair.gameObject);
        }

        _elements.Clear();
    }


    /// <summary>
    /// Check if any RevealUIElement already has the world coords passed in
    /// </summary>
    /// <param name="coords">World coordinates to check</param>
    /// <returns>True if any RevealUIElement has the coordinate specified</returns>
    private bool IsCoordInList(Vector3 coords) {
        return _elements.Any(t => t.coords == coords);
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
        foreach (var element in _elements) {
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
        this.coords = coords;
        this.crosshair = crosshair;
    }
}
