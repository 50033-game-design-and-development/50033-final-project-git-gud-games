using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXInteractable : MonoBehaviour, IInteractable {
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    private int state;
    private int cachedState = -1;

    public void OnInteraction() {
        if (state == cachedState) {
            return;
        }

        AudioClip clip = audioClips[state];
        if (clip != null) {
            StopCoroutine("ClearCache");
            cachedState = state;
            audioSource.PlayOneShot(clip);
            StartCoroutine("ClearCache", clip.length);
        }
    }
    
    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state = (state + 1) % audioClips.Count;
    }

    private IEnumerator ClearCache(float duration) {
        yield return new WaitForSeconds(duration);
        cachedState = -1;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
