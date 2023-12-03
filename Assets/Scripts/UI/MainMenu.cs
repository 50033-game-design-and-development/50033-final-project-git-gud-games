using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Animator cinemachineAnimator;
    public AudioSource sfxSource;
    public AudioSource ambienceSource;
    public AudioSource bgmSource;
    public AudioClip startSfx;
    public GameObject canvas;
    public CanvasGroup fadeCanvasGroup;
    
    public void StartButton() {
        canvas.SetActive(false);
        ambienceSource.Stop();
        bgmSource.Stop();
        sfxSource.PlayOneShot(startSfx);
        cinemachineAnimator.Play("Main Menu Black");
        StartCoroutine(LoadL0(4f));
    }

    public void QuitButton() {
        Application.Quit(0);
    }

    private IEnumerator LoadL0(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level 0");
    }
    
    private IEnumerator StartScreen() {
        Cursor.lockState = CursorLockMode.Confined;
        yield return new WaitForSeconds(2f);
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private void Start() {
        StartCoroutine(StartScreen());
    }
}
