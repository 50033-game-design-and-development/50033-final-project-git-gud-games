using System.Collections;
using UnityEngine;

public class AudioFade : MonoBehaviour {
    [SerializeField] private float maxVolume;
    [SerializeField] private float fadeDuration;
    private AudioSource audioSource;
    private float interval;

    public void FadeOut() {
        StartCoroutine("_FadeOut");
    }

    public void FadeIn() {
        StartCoroutine("_FadeIn");
    }

    private IEnumerator _FadeOut() {
        while (audioSource.volume > 0) {
            audioSource.volume -= interval;
            yield return new WaitForSeconds(0.1f);
        }
        //audioSource.Pause();
    }

    private IEnumerator _FadeIn() {
        //audioSource.UnPause();
        while (audioSource.volume < maxVolume) {
            audioSource.volume += interval;
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.volume = maxVolume;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        interval = maxVolume / fadeDuration * 0.1f;
    }
}
