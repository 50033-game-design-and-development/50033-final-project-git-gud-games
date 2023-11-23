using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    
    [SerializeField]
    private string sceneName;

    public void LoadSingle() {
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void LoadAdditive() {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
