using System.Collections;
using UnityEngine;

public class RunStinger : MonoBehaviour {
    private AudioSource _audioSource;
    private bool _enabled = true;
    private MonologueKey _key = MonologueKey.L1_LETTER;

    public void PlayStinger(MonologueKey key) {
        if (_enabled && key == _key) {
            StartCoroutine("DelayedStinger");
        }
    }

    private IEnumerator DelayedStinger() {
        yield return new WaitForSeconds(MonologueMap.Get(_key).audios[0].length - 0.5f);
        _audioSource.Play();
        _enabled = false;
    }

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }
}
