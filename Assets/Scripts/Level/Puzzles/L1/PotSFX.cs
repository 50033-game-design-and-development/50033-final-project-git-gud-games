using UnityEngine;

public class PotSFX : MonoBehaviour, IInteractable {
    private AudioSource audioSource;
    private bool canCook;

    public void OnInteraction() {
        if (canCook && !audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void SetCanCook(bool value) {
        canCook = value;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
