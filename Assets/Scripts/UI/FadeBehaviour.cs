using System.Collections;
using UnityEngine;

public class FadeBehaviour : MonoBehaviour {
    public CanvasGroup fadeOverlay;

    public bool playOnStart = true;

    public FadeType fadeType = FadeType.FadeOut;
    public enum FadeType {
        FadeIn,
        FadeOut,
    }

    public float duration = 2f;
    // Start is called before the first frame update
    void Start() {
        if (playOnStart) Fade();
    }

    public void Fade() {
        switch (fadeType) {
            case FadeType.FadeIn:
                StartCoroutine(FadeInCoroutine());
                break;
            case FadeType.FadeOut:
                StartCoroutine(FadeOutCoroutine());
                break;
            default: 
                StartCoroutine(FadeOutCoroutine());
                break;
        }
    }

    public void FadeIn() {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine() {
        fadeOverlay.alpha = 1f;
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = 1 - t / duration;
            yield return null;
        }
    }

    private IEnumerator FadeInCoroutine() {
        fadeOverlay.alpha = 0f;
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = t / duration;
            yield return null;
        }
    }

}
