using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutBehaviour : MonoBehaviour
{
    public CanvasGroup fadeOverlay;
    // Start is called before the first frame update
    void Start() {
        fadeOverlay.alpha = 1f;
        StartCoroutine(FadeIn());
        
    }

    private IEnumerator FadeIn() {
        float t = 0;
        while (t < 2) {
            t += Time.deltaTime;
            fadeOverlay.alpha = 1 - t / 2;
            yield return null;
        }

    }
}
