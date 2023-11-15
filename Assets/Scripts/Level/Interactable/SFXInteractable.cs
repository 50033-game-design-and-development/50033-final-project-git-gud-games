using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXInteractable : MonoBehaviour, IInteractable {
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    public AudioClip unlockAudio;
    private int state;

    public void OnInteraction() {
        AudioClip clip = audioClips[state];
        if (clip != null) {
            audioSource.PlayOneShot(audioClips[state]);
        }
    }

    public void playunlock()
    {
        StartCoroutine(playunlockScene());
        // audioSource.Stop();
        // audioSource.PlayOneShot(unlockAudio);
    }
    
    public IEnumerator playunlockScene()
    {
        audioSource.Stop();
        GetComponent<Animator>().SetTrigger("Unlock");
        audioSource.PlayOneShot(unlockAudio);
        yield return new WaitForSeconds(unlockAudio.length);
        SceneManager.LoadSceneAsync("BlankScene", LoadSceneMode.Single);
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state = (state + 1) % audioClips.Count;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    
    
}
