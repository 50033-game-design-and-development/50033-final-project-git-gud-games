using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Animator cinemachineAnimator;
    public AudioSource sfxSource;
    public AudioSource ambienceSource;
    public AudioSource bgmSource;
    public AudioClip startSfx;
    public GameObject canvas;
    public CanvasGroup fadeCanvasGroup;

    public void ContinueButton() {
        PlayTransition();
        GameState.inventory = new List<Inv.Collectable>(GameState.save.inventory);
        StartCoroutine(LoadLevel(GameState.save.level, 4f));
    }

    public void StartButton() {
        GameState.save.inventory.Clear();
        GameState.level = 0;
        PlayTransition();
        StartCoroutine(LoadLevel(0, 4f));
    }

    public void QuitButton() {
        DiscordController.ClearActivity();
        Application.Quit(0);
    }

    private void PlayTransition() {
        canvas.SetActive(false);
        ambienceSource.Stop();
        bgmSource.Stop();
        sfxSource.PlayOneShot(startSfx);
        cinemachineAnimator.Play("Main Menu Black");
    }

    private IEnumerator LoadLevel(int level, float time) {
        yield return new WaitForSeconds(time);
        GameState.level = level;
        SceneManager.LoadScene("LevelTxn");
    }
    
    private IEnumerator StartScreen() {
        Cursor.lockState = CursorLockMode.Confined;
        yield return new WaitForSeconds(1f);
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private void Start() {
        StartCoroutine(StartScreen());
        var btn = GameObject.Find("Continue Button");
        var continueButton = btn.GetComponent<Button>();
        var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (GameState.save.level == 0) {
            continueButton.interactable = false;
            txt.alpha = 0.5f;
        }
    }
}
