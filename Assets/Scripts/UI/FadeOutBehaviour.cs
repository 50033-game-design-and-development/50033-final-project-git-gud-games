using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInBehaviour : MonoBehaviour
{
    public CanvasGroup fadeOverlay;
    // Start is called before the first frame update
    void Start() {
        fadeOverlay.alpha = 0f;
        StartCoroutine(FadeIn());
        
    }

    private IEnumerator FadeIn() {
        float t = 0;
        while (t < 2) {
            t += Time.deltaTime;
            fadeOverlay.alpha = t / 2;
            yield return null;
        }
    }
}
