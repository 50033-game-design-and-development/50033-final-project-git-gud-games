using System.Collections.Generic;
using UnityEngine;

public class SFXInteractable : MonoBehaviour, IInteractable {
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    private int state;

    public void OnInteraction() {
        AudioClip clip = audioClips[state];
        if (clip != null) {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state = (state + 1) % audioClips.Count;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
