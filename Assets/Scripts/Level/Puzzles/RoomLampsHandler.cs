using System.Collections.Generic;
using UnityEngine;

namespace Level.Puzzles {
    public class RoomLampsHandler : MonoBehaviour {
        public string lampsTagName = "lamp";

        public void OnLightsToggle(bool turnOn) {
            var lampGameObjects = FindInChildren(
                gameObject, lampsTagName
            );
            
            foreach (var lampGameObject in lampGameObjects) {
                if (lampGameObject.GetComponent<Light>() != null) {
                    var lamp = lampGameObject.GetComponent<Light>();
                    lamp.enabled = turnOn;
                }
            }
        }
        
        private List<GameObject> FindInChildren(
            GameObject obj, string tagName, List<GameObject> taggedChildren = null
        ) {
            taggedChildren ??= new List<GameObject>();
            
            foreach (Transform child in obj.transform) {
                if (                    
                    child.gameObject.tag.Equals(tagName) &&
                    child.GetComponent<Light>() != null
                ) {
                    taggedChildren.Add(child.gameObject);
                }

                // Recursive call on the current child
                FindInChildren(child.gameObject, tagName, taggedChildren);
            }
            
            return taggedChildren;
        }
    }
}
