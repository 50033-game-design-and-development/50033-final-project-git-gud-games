using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour {
    public TextMeshProUGUI levelNum;
    public TextMeshProUGUI firstTxt;
    public TextMeshProUGUI secondTxt;
    public Image img;

    private string[] firstTxts = { "Memories,", "Not", "Escape..." };
    private string[] secondTxts = { "Awakening", "Alone", "& Despair" };
    private TextMeshProUGUI[] fadeInTxt;
    private TextMeshProUGUI[] otherTxt;

    private IEnumerator PlayAudio(float time) {
        yield return new WaitForSeconds(time);
        var src = GetComponent<AudioSource>();
        src.Play();
        StartCoroutine(TxtFade());
    }

    private IEnumerator TxtFade() {
        for (float a = 0f; a < 1f; a += 0.02f) {
            levelNum.alpha = a;
            otherTxt[GameState.level].alpha = a;
            var c = img.color;
            img.color = new Color(c.r, c.g, c.b, a);
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(BkgFade());
    }

    private IEnumerator BkgFade() {
        for (float a = 0f; a < 1f; a += 0.02f) {
            fadeInTxt[GameState.level].alpha = a;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(FadeAll());
    }

    private IEnumerator FadeAll() {
        yield return new WaitForSecondsRealtime(1f);

        for (float a = 1f; a > 0f; a -= 0.02f) {
            levelNum.alpha = a;
            firstTxt.alpha = a;
            secondTxt.alpha = a;
            var c = img.color;
            img.color = new Color(c.r, c.g, c.b, a);
            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadSceneAsync("Level "+GameState.level, LoadSceneMode.Single);
    }

    private void Start() {
        fadeInTxt = new []{ secondTxt, firstTxt, secondTxt };
        otherTxt = new []{ firstTxt, secondTxt, firstTxt };

        levelNum.text = "Chapter " + (GameState.level + 1);
        firstTxt.text = firstTxts[GameState.level];
        secondTxt.text = secondTxts[GameState.level];

        levelNum.alpha = 0;
        firstTxt.alpha = 0;
        secondTxt.alpha = 0;
        fadeInTxt[GameState.level].color = new Color(0.78f, 0f, 0f, 0f);

        StartCoroutine(PlayAudio(1f));
    }
}
