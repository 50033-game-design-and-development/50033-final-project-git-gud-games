using System.Collections.Generic;
using UnityEngine;

public class MonologueInteractable : MonoBehaviour, IInteractable {
    [SerializeField] private List<MonologueKey> monologueKeys;
    private int state;

    public void OnInteraction() {
        MonologueKey key = monologueKeys[state];
        if (key != MonologueKey.NULL) {
            Event.Global.showDialogue.Raise(key);
        }
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state = (state + 1) % monologueKeys.Count;
    }
}
