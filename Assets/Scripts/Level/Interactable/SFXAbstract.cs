using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SFXAbstract : MonoBehaviour {
    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource _audioSource;
    private int _state;
    private int _cachedState = -1;
    
    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        _state = (_state + 1) % audioClips.Count;
    }

    public float GetAudioLength() {
        if (_cachedState == -1) {
            return audioClips[_state].length;
        }
        return audioClips[_cachedState].length;
    }

    protected void PlaySFX() {
        if (_state == _cachedState) {
            return;
        }


        AudioClip clip = audioClips[_state];
        if (clip != null && _audioSource != null) {
            StopCoroutine("ClearCache");
            _cachedState = _state;
            Debug.Log(this.gameObject);
            _audioSource.PlayOneShot(clip);
            StartCoroutine("ClearCache", clip.length);
        }
    }

    private IEnumerator ClearCache(float duration) {
        yield return new WaitForSeconds(duration);
        _cachedState = -1;
    }

    protected void Start() {
        _audioSource = GetComponent<AudioSource>();
    }
}
