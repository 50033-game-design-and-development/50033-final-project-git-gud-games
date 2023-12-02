using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public void OnMainMenu() => SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);

    public void OnResume() => GameState.TogglePause();
}
