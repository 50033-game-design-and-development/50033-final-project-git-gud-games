using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonologueUI : MonoBehaviour {
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject page;

    protected float LENGTH_DIVISOR = 20.0f;

    private AudioSource audioSource;
    private TextMeshProUGUI subtitles;
    private Image background;
    private MonologueKey? cachedKey;
    private Image pageImage;
    private PlayerAction _playerAction;
    private bool inMonologue;

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

        if (cachedKey != null) {
            StopCoroutine("Monologue");
            StopCoroutine("WaitForMonologue");
            audioSource.Stop();
            Event.Global.endMonologue.Raise((MonologueKey)cachedKey);
        }

        StopCoroutine("EndMonologue");
        StartCoroutine("Monologue", monologueKey);
    }

    public void TogglePanel(bool value) {
        SetAlpha(value ? 1 : 0);
    }

    protected IEnumerator Monologue(MonologueKey monologueKey) {
        cachedKey = monologueKey;
        Monologue monologue = MonologueMap.Get(monologueKey);
        SetAlpha(1);

        for (int i = 0; i < monologue.strings.Count; i++) {
            subtitles.text = monologue.strings[i];
            AudioClip voiceLines = i < monologue.audios.Count ? monologue.audios[i] : null;
            float duration;

            if (voiceLines != null) {
                if (monologueKey != MonologueKey.L2_PC_AUDIO) {
                    audioSource.PlayOneShot(voiceLines);
                }
                duration = voiceLines.length;
            } else {
                // Set duration for unvoiced lines based on length of text
                duration = monologue.strings[i].Length / LENGTH_DIVISOR;
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
            inMonologue = true;
            StartCoroutine("WaitForMonologue", duration);
            while (inMonologue) {
                yield return null;
            }
            StopCoroutine("WaitForMonologue");
            audioSource.Stop();
        }

        StartCoroutine("EndMonologue", monologueKey);
    }

    private IEnumerator WaitForMonologue(float duration) {
        yield return new WaitForSeconds(duration);
        inMonologue = false;
    }

    private void SkipCurrentText() {
        if (GameState.isCutscenePlaying) return;
        inMonologue = false;
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

    protected virtual void SetAlpha(float value) {
        subtitles.alpha = value;
        background.color = new Color(0, 0, 0, value * 0.6f);
        if (page != null) {
            pageImage.color = new Color(1, 1, 1, value);
        }
    }

    private void Start() {
        _playerAction = new PlayerAction();
        _playerAction.Enable();
        _playerAction.gameplay.SkipText.performed += _ => SkipCurrentText();

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

    private void OnEnable() {
        if (_playerAction != null) {
            _playerAction.Enable();
        }
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
}
