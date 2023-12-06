using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public Animator cinemachineAnimator;
    public string blackState;

    public void OnMainMenu() {
        GameState.inventory.Clear();
        Event.Global.inventoryUpdate.Raise();

        GameState.TogglePause(false);
        GameState.HidePauseMenuUiElements();
        GameState.level = -1;

        cinemachineAnimator.Play(blackState);
        StartCoroutine(LoadMainMenu(1.0f));
    }

    private static IEnumerator LoadMainMenu(float seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void OnResume() => GameState.TogglePause();
}
