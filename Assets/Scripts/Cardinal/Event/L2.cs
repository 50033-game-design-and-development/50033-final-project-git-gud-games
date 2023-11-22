using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent fusePlugged;
        public static GameEvent paintingAnimationTrigger;

        [SerializeField] private GameEvent _fusePlugged;
        [SerializeField] private GameEvent _paintingAnimationTrigger;
        
        private void Start() {
            fusePlugged = _fusePlugged;
            paintingAnimationTrigger = _paintingAnimationTrigger;
        }
    }
}