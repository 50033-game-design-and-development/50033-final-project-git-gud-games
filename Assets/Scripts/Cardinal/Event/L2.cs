using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static GameEvent paintingAnimationTrigger;
        public static GameEvent seeFuseBox;

        [SerializeField] private GameEvent _paintingAnimationTrigger;
        [SerializeField] private GameEvent _seeFuseBox;
        
        private void Start() {
            paintingAnimationTrigger = _paintingAnimationTrigger;
            seeFuseBox = _seeFuseBox;
        }
    }
}
