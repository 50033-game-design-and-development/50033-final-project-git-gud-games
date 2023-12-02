using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public void OnMainMenu() {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        GameState.TogglePause();
    }

    public void OnResume() => GameState.TogglePause();
}
