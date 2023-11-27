using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Puzzles.L2_5 {
    public class Cauldron: MonoBehaviour, IDragDroppable {
        public GameObject murkyBubbles;
        public GameObject shinyWaters;
        public GameObject murkyWaters;
        public GameObject magicField;
        
        public Light waterGlowLight;
        public float glowDuration = 8.0f;
        public GameEvent onRitualStart;

        private float _startLightIntensity;
        private bool _lilyAdded = false;
        private bool _photoAdded = false;
        private bool _allCandlesLit = false;

        public GameEvent testEvent;

        private void Start() {
            _startLightIntensity = waterGlowLight.intensity;
        }

        public void OnAllCandlesLit() {
            _allCandlesLit = true;
        }

        public void test() {
            // raise test event if paper was dragged in
            if (!GameState.selectedInventoryItem.HasValue) {
                return;
            }
            
            var selectedInventoryItem = GameState.selectedInventoryItem.Value;
            if (selectedInventoryItem.itemType == InventoryItem.L0_Paper) {
                Debug.Log("ADD PAPER");
                testEvent.Raise();
            }
        }

        public void OnSolved() {
            Debug.Log("SOLVED");
        }

        public void OnDragDrop() {
            test();
            if (!GameState.selectedInventoryItem.HasValue) {
                return;
            }
            
            var selectedInventoryItem = GameState.selectedInventoryItem.Value;
            InventoryItem itemType = selectedInventoryItem.itemType;
            
            if (itemType == InventoryItem.L1_Lily) {
                murkyBubbles.SetActive(false);
                murkyWaters.SetActive(false);
                shinyWaters.SetActive(true);
                
                GameState.inventory.Remove(selectedInventoryItem);
                Event.Global.inventoryUpdate.Raise();
                _lilyAdded = true;
                return;
            }
            
            if (_lilyAdded && itemType == InventoryItem.L2_5_Photo) {
                waterGlowLight.enabled = true;
                magicField.SetActive(true);
                StartDimmingLight();
                
                GameState.inventory.Remove(selectedInventoryItem);
                Event.Global.inventoryUpdate.Raise();
                _photoAdded = true;
                return;
            }

            if (
                _photoAdded && _allCandlesLit &&
                itemType == InventoryItem.L2_5_Silver_key
            ) {
                onRitualStart.Raise(); 
                GameState.inventory.Remove(selectedInventoryItem);
                Event.Global.inventoryUpdate.Raise();
            }
        }

        private void StartDimmingLight() {
            StartCoroutine(DimLightOverTime(glowDuration));
        }
        
        private IEnumerator DimLightOverTime(float duration) {
            float startTime = Time.time;
            float timePassed = 0;

            while (timePassed < duration) {
                timePassed = Time.time - startTime;
                    
                float oscillator = 1 + 0.3f * (float) Math.Sin(timePassed * 8);
                waterGlowLight.intensity = Mathf.Lerp(
                    _startLightIntensity, 0, timePassed / duration
                ) * oscillator;
                
                yield return null; // Wait until the next frame
            }

            waterGlowLight.intensity = 0; // Ensure the intensity is set to 0
        }
    }
}
