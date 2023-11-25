using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    
    [SerializeField]
    private string sceneName;

    public void SetScene(string sceneName) {
        // Commented out because ending scenes are not in build settings
        // if (SceneManager.GetSceneByName(sceneName).IsValid())
        this.sceneName = sceneName;
    }

    public void LoadSingle() {
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void LoadAdditive() {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
