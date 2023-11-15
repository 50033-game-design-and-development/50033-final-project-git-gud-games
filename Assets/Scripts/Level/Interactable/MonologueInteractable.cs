using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonologueInteractable : MonoBehaviour, IInteractable {
    public List<MonologueKey> monologueKeys;
    public int state;

    public virtual void OnInteraction() {
        int key = (int)monologueKeys[state];
        if (key != -1) {
            Event.showDialogue.Raise(key);
        }
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state = (state + 1) % monologueKeys.Count;
    }
}
