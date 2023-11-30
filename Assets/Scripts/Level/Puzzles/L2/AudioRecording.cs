using System.Collections;
using UnityEngine;

public class AudioRecording : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    private MonologueKey audioKey = MonologueKey.L2_PC_AUDIO;

    public void StartRecording(MonologueKey monologueKey) {
        if (monologueKey == audioKey) {
            StartCoroutine("Recording");
        }
        else if (monologueKey == MonologueKey.TERMINATE) {
            StopCoroutine("Recording");
            audioSource.Stop();
        }
    }

    private IEnumerator Recording() {
        Monologue monologue = MonologueMap.Get(audioKey);

        for (int i = 0; i < monologue.audios.Count; i++) {
            AudioClip voiceLines = monologue.audios[i];
            audioSource.PlayOneShot(voiceLines);

            // Wait for monologue to be spoken completely
            yield return new WaitForSeconds(voiceLines.length);
        }

        Event.Global.endMonologue.Raise(audioKey);
    }
}
