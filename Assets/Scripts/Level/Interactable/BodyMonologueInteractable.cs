using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyMonologueInteractable : MonologueInteractable {
    public List<MonologueKey> monologueKeys;
    private int state = 2;

    public new void OnInteraction() {
        Event.showDialogue.Raise((int)monologueKeys[state]);
        if (state == 3) {
            state = 4;
            // Play audio source
            // Add key to inventory
        }
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state++;
    }
}
