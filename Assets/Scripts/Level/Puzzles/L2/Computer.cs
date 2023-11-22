using System.Collections;
using UnityEngine;

public class Computer : MonoBehaviour {

    [SerializeField] private Canvas startupOSCanvas;

    public void OnFloppyInserted() {
        StartCoroutine("LoadStartupScreen");
    }

    public IEnumerator LoadStartupScreen() {
        startupOSCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        startupOSCanvas.gameObject.SetActive(false);
    }
}