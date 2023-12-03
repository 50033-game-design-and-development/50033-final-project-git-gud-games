using UnityEngine;

public class InventorySFX : MonoBehaviour {
    [SerializeField] private AudioClip _openSFX;
    [SerializeField] private AudioClip _closeSFX;
    private AudioSource _audioSource;
    private bool _localState;
    private bool _onPasswordScreen;
    private bool _usingPC;
    public bool UsingPC {
        get => _usingPC;
        set => _usingPC = true;
    }

    public void PlaySFX() {
        // Ignore inventory updates due to picking up items
        if (!(_localState ^ GameState.isInventoryOpened)) {
            return;
        }

        // Ignore inventory updates when focusing on puzzle screen
        if (_onPasswordScreen && _usingPC) {
            return;
        }

        if (GameState.isInventoryOpened) {
            _audioSource.PlayOneShot(_openSFX);
        } else {
            _audioSource.PlayOneShot(_closeSFX);
        }
        _localState = GameState.isInventoryOpened;
    }

    public void DisableSFX(bool value) {
        _onPasswordScreen = value;
    }

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!GameState.isPuzzleLocked) {
            _usingPC = false;
        }
    }
}
