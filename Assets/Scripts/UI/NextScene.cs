using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour, IInteractable, IClickable {
    [SerializeField] private string sceneName;

    public int sceneLoadDelay = 5;

    private bool _allowSceneChange;

    public void SetScene(string sceneName) {
        // Commented out because ending scenes are not in build settings
        // if (SceneManager.GetSceneByName(sceneName).IsValid())
        this.sceneName = sceneName;
    }

    public void LoadSingle() {
        GameState.isInventoryOpened = false;
        GameState.isPuzzleLocked = false;
        GameState.isCutscenePlaying = false;

        var key = GameState.inventory.Find(x => x.itemType == InventoryItem.L0_Key);
        var vial = GameState.inventory.Find(x => x.itemType == InventoryItem.L1_Vial_filled);

        GameState.inventory.Clear();

        if (!key.Equals(default(Inv.Collectable))) {
            GameState.inventory.Add(key);
        }
        if (!vial.Equals(default(Inv.Collectable))) {
            GameState.inventory.Add(vial);
        }

        GameState.permLockMouse = false;

        if (!sceneName.StartsWith("Level")) {
            GameState.level = -1;
            GameState.inventory.Clear();
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            return;
        }
        var level = sceneName.Substring(sceneName.Length - 1, 1);
        int.TryParse(level, out GameState.level);

        GameState.save.inventory = new List<Inv.Collectable>(GameState.inventory);
        GameState.save.level = GameState.level;

        SceneManager.LoadSceneAsync("LevelTxn", LoadSceneMode.Single);
    }

    private IEnumerator LoadWithDelay(float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        LoadSingle();
    }

    public void AllowSceneChange() {
        _allowSceneChange = true;
    }

    public void OnInteraction() {
        if(!_allowSceneChange) return;
        StartCoroutine(LoadWithDelay(sceneLoadDelay));
    }

    public void OnClick() => OnInteraction();
}
