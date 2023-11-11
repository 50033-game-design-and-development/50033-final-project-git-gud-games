using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource playerAudioSource;
    public AudioClip leftWalkSound;
    public AudioClip rightWalkSound;
    private bool turn = true;
    
    /// <summary>
    /// take alternate turns to play different footstep sound to make it sound more natural
    /// checks if the audio is already playing to prevent distorted sound effect as its called frame
    /// </summary>
    public void PlayFootStepSFX() {
        if (!playerAudioSource.isPlaying) {
            if (turn) {
                playerAudioSource.PlayOneShot(leftWalkSound);
                turn = !turn;
            }
            else {
                playerAudioSource.PlayOneShot(rightWalkSound);
                turn = !turn;
            }
        }
    }
}
