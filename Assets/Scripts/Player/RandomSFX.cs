using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFX : MonoBehaviour {
    [SerializeField] private List<AudioClip> _sfxList;
    [SerializeField] private List<AudioSource> _sourceList;

    private IEnumerator PlaySFX() {
        while (true) {
            yield return new WaitForSeconds(30);

            // Select an audio clip to play
            // 50% chance to not play a SFX
            int roll = Random.Range(0, _sfxList.Count * 2);
            if (roll < _sfxList.Count) {
                AudioClip clip = _sfxList[roll];

                // Select a random audio source
                roll = Random.Range(0, _sourceList.Count);
                AudioSource source = _sourceList[roll];
                source.PlayOneShot(clip);
            }
        }
    }

    private void Start() {
        StartCoroutine("PlaySFX");
    }
}
