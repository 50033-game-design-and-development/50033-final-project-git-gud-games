using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private List<AudioClip> walkSounds;

    /// <summary>
    /// plays a random footstep SFX from the list
    /// to be called whenever the camera bobs down to the lowest position, so that the movement and SFX are in sync
    /// </summary>
    public void PlayFootStepSFX() {
        playerAudioSource.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Count)]);
    }
}
