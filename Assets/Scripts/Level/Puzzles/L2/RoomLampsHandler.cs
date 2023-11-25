using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Puzzles {
    public class RoomLampsHandler : MonoBehaviour {
        public string lampsTagName = "lamp";
        public Material lampsOnMaterial;
        public Material lampsOffMaterial;

        public virtual void OnLightsToggle(bool turnOn) {
            var lampGameObjects = FindInChildren(
                gameObject, lampsTagName
            );
            
            foreach (var lampGameObject in lampGameObjects) {
                if (lampGameObject.TryGetComponent<Light>(out var lamp)) {
                    lamp.enabled = turnOn;
                } else if (
                    lampGameObject.TryGetComponent<Renderer>(out var renderer)
                ) {
                    renderer.material = turnOn ? 
                        lampsOnMaterial : lampsOffMaterial;
                }

            }
        }
        
        private List<GameObject> FindInChildren(
            GameObject obj, string tagName, List<GameObject> taggedChildren = null
        ) {
            taggedChildren ??= new List<GameObject>();
            
            foreach (Transform child in obj.transform) {
                if (child.gameObject.tag.Equals(tagName)) {
                    taggedChildren.Add(child.gameObject);
                }

                // Recursive call on the current child
                FindInChildren(child.gameObject, tagName, taggedChildren);
            }
            
            return taggedChildren;
        }
    }
}
