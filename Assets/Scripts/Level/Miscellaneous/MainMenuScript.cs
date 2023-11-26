using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public Animator cinemachineAnimator;
    public AudioSource sfxSource;
    public AudioSource ambienceSource;
    public AudioSource bgmSource;
    public AudioClip startSfx;
    public GameObject canvas;
    
    public void StartButton() {
        canvas.SetActive(false);
        ambienceSource.Stop();
        bgmSource.Stop();
        sfxSource.PlayOneShot(startSfx);
        cinemachineAnimator.Play("Main Menu Black");
        StartCoroutine(LoadScene(4f));
    }

    public void QuitButton() {
        Application.Quit(0);
    }

    private IEnumerator LoadScene(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level 0");
    }
}
