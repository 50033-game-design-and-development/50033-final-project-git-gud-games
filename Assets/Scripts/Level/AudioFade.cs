using System.Collections;
using UnityEngine;

public class AudioFade : MonoBehaviour {
    private AudioSource audioSource;

    public void FadeOut() {
        StartCoroutine("_FadeOut");
    }

    public void FadeIn() {
        StartCoroutine("_FadeIn");
    }

    private IEnumerator _FadeOut() {
        while (audioSource.volume > 0) {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        //audioSource.Pause();
    }

    private IEnumerator _FadeIn() {
        //audioSource.UnPause();
        while (audioSource.volume < 1) {
            audioSource.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
