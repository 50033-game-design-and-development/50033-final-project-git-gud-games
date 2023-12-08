using System;
using System.Collections;
using UnityEngine;

namespace Level.Puzzles.L2_5 {
    public class Cauldron : MonoBehaviour, IDragDroppable {
        public GameObject murkyBubbles;
        public GameObject shinyWaters;
        public GameObject murkyWaters;
        public GameObject magicField;

        // light intensity after placing the lily into cauldron
        public float startLightIntensity = 0.5f;
        // light intensity right after placing the photo into cauldron
        public float startGlowLightIntensity = 5.0f;
        // light intensity after pulsating glowing animation
        public float endGlowLightIntensity = 1.0f;

        public Light waterGlowLight;
        public float glowDuration = 8.0f;

        [SerializeField] private AudioClip shinyCauldronPhase1Clip;
        [SerializeField] private AudioClip shinyCauldronPhase2Clip;
        [SerializeField] private AudioClip shinyCauldronPhase3Clip;
        private AudioSource _audioSource;

        private bool _lilyAdded = false;
        private bool _photoAdded = false;
        private bool _allCandlesLit = false;

        public GameEvent testEvent;
        
        public void OnAllCandlesLit() {
            _allCandlesLit = true;
        }
        
        public void OnDragDrop() {
            if (!GameState.selectedInventoryItem.HasValue) {
                return;
            }
            
            var selectedInventoryItem = GameState.selectedInventoryItem.Value;
            InventoryItem itemType = selectedInventoryItem.itemType;
            
            if (_allCandlesLit && itemType == InventoryItem.L1_Vial_filled) {
                murkyBubbles.SetActive(false);
                murkyWaters.SetActive(false);
                shinyWaters.SetActive(true);
                
                waterGlowLight.enabled = true;
                waterGlowLight.intensity = startLightIntensity;
                
                GameState.inventory.Remove(selectedInventoryItem);
                Event.Global.inventoryUpdate.Raise();
                _lilyAdded = true;

                _audioSource.clip = shinyCauldronPhase1Clip;
                _audioSource.Play();
                return;
            }
            
            if (_lilyAdded && itemType == InventoryItem.L2_5_Photo) {
                StartDimmingLight();
                GameState.inventory.Remove(selectedInventoryItem);
                Event.Global.inventoryUpdate.Raise();
                _photoAdded = true;

                _audioSource.clip = shinyCauldronPhase2Clip;
                _audioSource.Play();
                return;
            }

            if (
                _photoAdded && _allCandlesLit &&
                itemType == InventoryItem.L0_Key
            ) {
                magicField.SetActive(true);
                Event.L2.solvedP6.Raise();
                GameState.inventory.Remove(selectedInventoryItem);

                _audioSource.clip = shinyCauldronPhase3Clip;
                _audioSource.Play();
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

                float progress = timePassed / duration;
                float oscillateIntensity = 0.4f * (1.0f - progress);
                float oscillator = (
                    1 + oscillateIntensity * (float) Math.Sin(timePassed * 8)
                );
                    
                waterGlowLight.intensity = Mathf.Lerp(
                    startGlowLightIntensity, endGlowLightIntensity, progress
                ) * oscillator;
                
                yield return null;
            }

            waterGlowLight.intensity = endGlowLightIntensity;
        }

        private void Start() {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
