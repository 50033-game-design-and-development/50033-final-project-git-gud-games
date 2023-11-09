using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MonologueUI : MonoBehaviour {
    public MonologueMap monologueMap;
    private AudioSource audioSource;
    private TextMeshProUGUI subtitles;
    private Image background;

    public void StartMonologue(int monologueKey) {
        StopCoroutine("EndMonologue");

        subtitles.text = monologueMap.textList[monologueKey];
        SetAlpha(1);

        // Disabled these lines to prevent breakage, DO NOT DELETE
        //AudioClip voiceLines = monologueMap.audioList[monologueKey];
        //audioSource.PlayOneShot(voiceLines);
        //StartCoroutine("EndMonologue", voiceLines.length);

        StartCoroutine("EndMonologue", 1);
    }

    private IEnumerator EndMonologue(float duration) {
        // Wait for monologue to be spoken completely
        yield return new WaitForSeconds(duration);

        // Fade out monologue panel
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
