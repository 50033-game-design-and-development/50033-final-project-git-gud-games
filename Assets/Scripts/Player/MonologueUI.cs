using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MonologueUI : MonoBehaviour {
    public AudioMap audioMap;
    public StringMap stringMap;
    private AudioSource audioSource;
    private TextMeshProUGUI subtitles;
    private Image background;

    public void StartMonologue(int monologueKey) {
        StopCoroutine("Monologue");
        StopCoroutine("EndMonologue");
        StartCoroutine("Monologue", monologueKey);
    }

    private IEnumerator Monologue(int monologueKey) {
        string unparsedText = stringMap.textList[monologueKey];
        string[] monologues = unparsedText.Split('|');
        SetAlpha(1);

        foreach (string monologue in monologues) {
            subtitles.text = monologue;

            // Disabled these lines to prevent breakage, DO NOT DELETE
            //AudioClip voiceLines = audioMap.audioList[monologueKey];
            //audioSource.PlayOneShot(voiceLines);

            // Wait for monologue to be spoken completely
            //yield return new WaitForSeconds(voiceLines.length);
            yield return new WaitForSeconds(1);
        }

        StartCoroutine("EndMonologue");
    }

    // Fade out monologue panel
    private IEnumerator EndMonologue() {
        for (float alpha = 1; alpha > 0; alpha -= 0.05f) {
            SetAlpha(alpha);
            yield return new WaitForSeconds(0.05f);
        }
        subtitles.text = "";
    }

    private void SetAlpha(float value) {
        subtitles.alpha = value;
        background.color = new Color(0, 0, 0, value * 0.6f);
    }

    private void Start () {
        audioSource = GetComponent<AudioSource>();
        subtitles = GetComponentInChildren<TextMeshProUGUI>();
        background = GetComponent<Image>();

        subtitles.text = "";
        SetAlpha(0);
    }
}
