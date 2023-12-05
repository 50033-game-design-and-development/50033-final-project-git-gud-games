using UnityEngine;

public class CutsceneStateEnabler : MonoBehaviour {

    public void EnableCutsceneState() {
        if (GameState.isInventoryOpened) {
            GameState.ToggleInventory();
        }

        GameState.isCutscenePlaying = true;

        Event.Global.hideAll.Raise();
    }

    public void ExitCutsceneState() {
        GameState.isCutscenePlaying = false;
    }
}