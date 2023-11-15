using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonologueInteractable : MonoBehaviour, IInteractable {
    public List<MonologueKey> monologueKeys;
    public int state;

    public virtual void OnInteraction() {
        Event.showDialogue.Raise((int)monologueKeys[state]);
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state++;
    }
}
