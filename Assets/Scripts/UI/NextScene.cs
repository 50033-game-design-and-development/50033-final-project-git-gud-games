using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour, IInteractable, IClickable {
    [SerializeField] private string sceneName;

    public int sceneLoadDelay = 5;

    public void LoadSingle() {
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;

        var key = GameState.inventory.Find(x => x.itemType == InventoryItem.L0_Key);
        var vial = GameState.inventory.Find(x => x.itemType == InventoryItem.L1_Vial_filled);

        GameState.inventory.Clear();

        if (!key.Equals(default(Inv.Collectable))) {
            GameState.inventory.Add(key);
        }
        if (!vial.Equals(default(Inv.Collectable))) {
            GameState.inventory.Add(vial);
        }

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    // public void LoadAdditive() {
    //     SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    // }

    private IEnumerator LoadWithDelay(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        LoadSingle();
    }

    public void OnInteraction() {
        StartCoroutine(LoadWithDelay(sceneLoadDelay));
    }

    public void OnClick() => OnInteraction();
}
