using System.Collections;
using UnityEngine;

public class FadeBehaviour : MonoBehaviour {
    public CanvasGroup fadeOverlay;

    public bool playOnStart = true;

    public FadeType fadeType = FadeType.FadeOut;
    public enum FadeType {
        FadeIn,
        FadeOut,
        FadeInOut,
    }

    public float duration = 2f;
    // Start is called before the first frame update
    void Start() {
        if (playOnStart) Fade();
    }

    public void Fade() {
        Debug.Log("Fade");
        switch (fadeType) {
            case FadeType.FadeIn:
                fadeOverlay.alpha = 0f;
                StartCoroutine(FadeIn());
                break;
            case FadeType.FadeOut:
                fadeOverlay.alpha = 1f;
                StartCoroutine(FadeOut());
                break;
            case FadeType.FadeInOut:
                fadeOverlay.alpha = 0f;
                StartCoroutine(FadeInOut());
                break;
            default: 
                fadeOverlay.alpha = 1f;
                StartCoroutine(FadeOut());
                break;
        }
    }

    private IEnumerator FadeOut() {
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = 1 - t / duration;
            yield return null;
        }
    }

    private IEnumerator FadeIn() {
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = t / duration;
            yield return null;
        }
    }

    private IEnumerator FadeInOut() {
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = t / duration;
            yield return null;
        }
        t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = 1 - t / duration;
            yield return null;
        }
    }
}
