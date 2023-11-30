using UnityEngine;

public class DeadBody : MonoBehaviour {
    private MonologueKeyGameEventListener listener;
    private CutsceneInteractable cutscene;

    public void StartCutscene(MonologueKey key) {
        if (key != MonologueKey.L0_CORPSE_AFTER_P1) {
            return;
        }
        cutscene.IncrementState();
        cutscene.OnInteraction();
        listener.enabled = false;
    }

    private void Start() {
        listener = GetComponent<MonologueKeyGameEventListener>();
        cutscene = GetComponent<CutsceneInteractable>();
    }
}
