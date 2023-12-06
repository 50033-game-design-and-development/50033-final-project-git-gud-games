using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecording : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    private MonologueKey audioKey = MonologueKey.L2_PC_AUDIO;
    private PlayerAction _playerAction;
    private bool inMonologue;

    public void StartRecording(MonologueKey monologueKey) {
        if (monologueKey == audioKey) {
            StartCoroutine("Recording");
        }
        else if (monologueKey == MonologueKey.TERMINATE) {
            StopCoroutine("Recording");
            StopCoroutine("WaitForMonologue");
            audioSource.Stop();
        }
    }

    private IEnumerator Recording() {
        Monologue monologue = MonologueMap.Get(audioKey);

        for (int i = 0; i < monologue.audios.Count; i++) {
            AudioClip voiceLines = monologue.audios[i];
            audioSource.PlayOneShot(voiceLines);

            // Wait for monologue to be spoken completely
            inMonologue = true;
            StartCoroutine("WaitForMonologue", voiceLines.length);
            while (inMonologue) {
                yield return null;
            }
            StopCoroutine("WaitForMonologue");
            audioSource.Stop();
        }

        Event.Global.endMonologue.Raise(audioKey);
    }

    private IEnumerator WaitForMonologue(float duration) {
        yield return new WaitForSeconds(duration);
        inMonologue = false;
    }

    private void SkipCurrentText() {
        if (GameState.isCutscenePlaying) return;
        inMonologue = false;
    }

    private void Start() {
        _playerAction = new PlayerAction();
        _playerAction.Enable();
        _playerAction.gameplay.SkipText.performed += _ => SkipCurrentText();
    }

    private void OnEnable() {
        if (_playerAction != null) {
            _playerAction.Enable();
        }
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
}
