using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static GameEvent paintingAnimationTrigger;
        [SerializeField] private GameEvent _paintingAnimationTrigger;
        
        private void Start() {
            paintingAnimationTrigger = _paintingAnimationTrigger;
        }
    }
}