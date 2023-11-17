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
                StartCoroutine(FadeIn());
                break;
            case FadeType.FadeOut:
                StartCoroutine(FadeOut());
                break;
            default: 
                StartCoroutine(FadeOut());
                break;
        }
    }

    private IEnumerator FadeOut() {
        fadeOverlay.alpha = 1f;
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = 1 - t / duration;
            yield return null;
        }
    }

    private IEnumerator FadeIn() {
        fadeOverlay.alpha = 0f;
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            fadeOverlay.alpha = t / duration;
            yield return null;
        }
    }

}
