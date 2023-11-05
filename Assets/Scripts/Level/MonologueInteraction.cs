using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonologueInteraction : MonoBehaviour, IInteractable {
    public int[] monologueKeys;
    public UnityEvent<int> OnMonologue;
    private int state;

    public void OnInteraction() {
        OnMonologue.Invoke(monologueKeys[state]);
    }

    // To be called by event listener so that monologue changes based on game state
    public void IncrementState() {
        state++;
    }
}
