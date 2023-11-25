using System;
using UnityEngine;

namespace Level.Interactable {
    public class LightInteractable : MonoBehaviour, IInteractable {
        public bool switchedOn = true;
        public BoolGameEvent onSwitchToggle;

        public void Start() {
            onSwitchToggle.Raise(switchedOn);
        }

        public void OnInteraction() {
            // Debug.Log("LIGHT SWITCH INTERACT");
            switchedOn = !switchedOn;
            onSwitchToggle.Raise(switchedOn);
        }
    }
}
