using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class L2Silhouette : MonoBehaviour {
    [Tooltip("Probability of a silhouette appearing")]
    public float appearanceProbability = 0.3f;
    
    [Tooltip("Minimum time of visibility")]
    public float minAppearanceTime = 0.5f;
    
    [Tooltip("Maximum time of visibility")]
    public float maxAppearanceTime = 10f;
    
    [Tooltip("Optional, if appear, play the audio source's clip")]
    public AudioSource audioSource;
    
    [Tooltip("Probability of the audio source playing")]
    public float audioProbabilty = 0.5f;

    private SpriteRenderer _silhouette;
    
    // called when lights are switched on or off. Silhouette only appears when lights are off
    public void Visible(bool state) {
        if (Random.value < appearanceProbability && !state) {
            _silhouette.color = Color.black;
            _silhouette.enabled = true;
            StartCoroutine(VisibleTime(Random.Range(minAppearanceTime, maxAppearanceTime)));
            if (audioSource != null) {
                if(Random.value < audioProbabilty) audioSource.PlayOneShot(audioSource.clip);
            }
        }
        else {
            _silhouette.enabled = false;
        }
    }
    
    // Silhouette disappears after a random time or when player enters collider or when lights are on.
    private IEnumerator VisibleTime(float time) {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeOutCoroutine(2.5f));
    }

    // When player enters its collider, silhouette disappears
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            StartCoroutine(FadeOutCoroutine(1f));
        }
    }

    IEnumerator FadeOutCoroutine(float fadeDuration) {
        Color originalColor = _silhouette.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            _silhouette.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _silhouette.enabled = false;
    }

    private void Start() {
        _silhouette = this.gameObject.GetComponent<SpriteRenderer>();
    }
}
