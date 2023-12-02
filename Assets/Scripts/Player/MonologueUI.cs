using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MonologueUI : MonoBehaviour {
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject page;

    private AudioSource audioSource;
    private TextMeshProUGUI subtitles;
    private Image background;
    private MonologueKey? cachedKey;
    private Image pageImage;

    public void StartMonologue(MonologueKey monologueKey) {
        if (monologueKey == cachedKey) {
            return;
        }

        if (monologueKey == MonologueKey.TERMINATE) {
            if (cachedKey == MonologueKey.L2_PC_AUDIO) {
                StopCoroutine("Monologue");
                StartCoroutine("EndMonologue", MonologueKey.NULL);
                audioSource.Stop();
            }
            return;
        }

        StopCoroutine("Monologue");
        StopCoroutine("EndMonologue");
        audioSource.Stop();
        StartCoroutine("Monologue", monologueKey);
    }

    protected IEnumerator Monologue(MonologueKey monologueKey) {
        cachedKey = monologueKey;
        Monologue monologue = MonologueMap.Get(monologueKey);
        SetAlpha(1);

        for (int i = 0; i < monologue.strings.Count; i++) {
            subtitles.text = monologue.strings[i];
            AudioClip voiceLines = monologue.audios[i];
            float duration;

            if (voiceLines != null) {
                if (monologueKey != MonologueKey.L2_PC_AUDIO) {
                    audioSource.PlayOneShot(voiceLines);
                }
                duration = voiceLines.length;
            } else {
                // Set duration for unvoiced lines based on length of text
                duration = monologue.strings[i].Length / 20.0f;
            }

            if (arrow != null && page != null) {
                if (i == monologue.strings.Count - 1) {
                    arrow.SetActive(false);
                    page.SetActive(true);
                } else {
                    arrow.SetActive(true);
                    page.SetActive(false);
                }
            }

            // Wait for monologue to be spoken completely
            yield return new WaitForSeconds(duration);
        }

        StartCoroutine("EndMonologue", monologueKey);
    }

    // Fade out monologue panel
    private IEnumerator EndMonologue(MonologueKey key) {
        cachedKey = null;
        Event.Global.endMonologue.Raise(key);
        for (float alpha = 1; alpha > -0.1f; alpha -= 0.05f) {
            SetAlpha(alpha);
            yield return new WaitForSeconds(0.05f);
        }
        subtitles.text = "";

        if (page != null) {
            page.SetActive(false);
        }
    }

    private void SetAlpha(float value) {
        subtitles.alpha = value;
        background.color = new Color(0, 0, 0, value * 0.6f);
        if (page != null) {
            pageImage.color = new Color(1, 1, 1, value);
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        subtitles = GetComponentInChildren<TextMeshProUGUI>();
        background = GetComponent<Image>();
        subtitles.text = "";

        if (arrow != null && page != null) {
            pageImage = page.GetComponent<Image>();
            arrow.SetActive(false);
            page.SetActive(false);
        }

        SetAlpha(0);
    }
}
