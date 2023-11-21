using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    
    [SerializeField]
    private string sceneName;

    public void LoadSingle() {
        GameState.inventoryOpened = false;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void LoadAdditive() {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }


}
