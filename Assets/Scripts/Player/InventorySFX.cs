using UnityEngine;

public class InventorySFX : MonoBehaviour {
    [SerializeField] private AudioClip _openSFX;
    [SerializeField] private AudioClip _closeSFX;
    private AudioSource audioSource;
    private bool localState;

    public void PlaySFX() {
        // Ignore inventory updates due to picking up items
        if (!(localState ^ GameState.isInventoryOpened)) {
            return;
        }

        if (GameState.isInventoryOpened) {
            audioSource.PlayOneShot(_openSFX);
        } else {
            audioSource.PlayOneShot(_closeSFX);
        }
        localState = GameState.isInventoryOpened;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
