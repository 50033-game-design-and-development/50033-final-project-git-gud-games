using System.Collections;
using UnityEngine;
public class SimplePageFlip : MonoBehaviour {
    public float flipSpeed = 0.1f; // Adjust the speed of the flip as needed
    public GameObject topPage;
    public GameObject bottomPage;
    private bool _isFlipping = false;
    
    public void LeftFlip() {
        if (!_isFlipping) {
            StartCoroutine(FlipPage(bottomPage,topPage, 0, +180));
        }
    }

    public void RightFlip() {
        if (!_isFlipping) {
            StartCoroutine(FlipPage(topPage,bottomPage, +180, 0));
        }
    }

    IEnumerator FlipPage(GameObject newPage, GameObject currentPage, float startRotation, float endRotation) {
        SetPageActive(newPage, true);
        _isFlipping = true;

        float startTime = Time.time;
        float journeyLength = 1f / flipSpeed;

        while (Time.time - startTime < journeyLength) {
            float distCovered = (Time.time - startTime) * flipSpeed;
            float fracJourney = distCovered / journeyLength;

            // Rotate the page smoothly
            float targetRotation = Mathf.Lerp(startRotation, endRotation, fracJourney);
            transform.localRotation = Quaternion.Euler(0, 0, targetRotation);

            yield return null;
        }

        // Ensure the rotation is exactly at the endRotation
        transform.localRotation = Quaternion.Euler(0, 0, endRotation);

        _isFlipping = false;
        SetPageActive(currentPage, false);
    }

    private void SetPageActive(GameObject page, bool isActive) {
        if (page != null) {
            page.GetComponent<Collider>().enabled = isActive;
            page.GetComponent<MeshRenderer>().enabled = isActive;
        }
    }
    
    private void Start() {
        SetPageActive(bottomPage, false);
    }

}
