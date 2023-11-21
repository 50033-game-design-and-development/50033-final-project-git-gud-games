using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MonologueUI : MonoBehaviour {
    private AudioSource audioSource;
    private TextMeshProUGUI subtitles;
    private Image background;
    private MonologueKey? cachedKey;

    public void StartMonologue(MonologueKey monologueKey) {
        if (monologueKey == cachedKey) {
            return;
        }
        StopCoroutine("Monologue");
        StopCoroutine("EndMonologue");
        StartCoroutine("Monologue", monologueKey);
    }

    private IEnumerator Monologue(MonologueKey monologueKey) {
        cachedKey = monologueKey;
        Monologue monologue = MonologueMap.Get(monologueKey);
        SetAlpha(1);

        for (int i = 0; i < monologue.strings.Count; i++) {
            subtitles.text = monologue.strings[i];
            AudioClip voiceLines = monologue.audios[i];
            float duration;

            if (voiceLines != null) {
                audioSource.PlayOneShot(voiceLines);
                duration = voiceLines.length;
            } else {
                // Set duration for unvoiced lines based on length of text
                duration = monologue.strings[i].Length / 15.0f;
            }

            // Wait for monologue to be spoken completely
            yield return new WaitForSeconds(duration);
        }

        StartCoroutine("EndMonologue");
    }

    // Fade out monologue panel
    private IEnumerator EndMonologue() {
        cachedKey = null;
        for (float alpha = 1; alpha > -0.1f; alpha -= 0.05f) {
            SetAlpha(alpha);
            yield return new WaitForSeconds(0.05f);
        }
        subtitles.text = "";
    }

    private void SetAlpha(float value) {
        subtitles.alpha = value;
        background.color = new Color(0, 0, 0, value * 0.6f);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        subtitles = GetComponentInChildren<TextMeshProUGUI>();
        background = GetComponent<Image>();

        subtitles.text = "";
        SetAlpha(0);
    }
}
