using UnityEngine;

namespace Level.Interactable {
    public class LightInteractable : MonoBehaviour, IInteractable {
        private bool _switchedOn = true;
        public BoolGameEvent onSwitchToggle;
        
        public void OnInteraction() {
            // Debug.Log("LIGHT SWITCH INTERACT");
            _switchedOn = !_switchedOn;
            onSwitchToggle.Raise(_switchedOn);
        }
    }
}
