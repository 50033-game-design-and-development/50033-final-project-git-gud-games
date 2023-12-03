using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour {
    public TextMeshProUGUI levelNum;
    public TextMeshProUGUI firstTxt;
    public TextMeshProUGUI secondTxt;
    public Image img;

    private string[] firstTxts = { "Memories,", "Not", "Escape" };
    private string[] secondTxts = { "Awakening", "Alone", "Despair" };
    private TextMeshProUGUI[] fadeInTxt;
    private TextMeshProUGUI[] otherTxt;

    private IEnumerator TxtFade() {
        for (float a = 0f; a < 1f; a += 0.02f) {
            levelNum.alpha = a;
            otherTxt[GameState.level].alpha = a;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(BkgFade());
    }

    private IEnumerator BkgFade() {
        for (float a = 0f; a < 1f; a += 0.02f) {
            // img.color = new Color(a, 0, 0, 0.5f);
            fadeInTxt[GameState.level].alpha = a;
            yield return new WaitForSeconds(0.05f);
        }
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

        StartCoroutine(TxtFade());
    }
}
