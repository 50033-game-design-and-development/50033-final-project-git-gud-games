using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent fusePlugged;
        public static GameEvent LoggedIn;
        public static GameEvent paintingAnimationTrigger;

        [SerializeField] private GameEvent _fusePlugged;
        [SerializeField] private GameEvent _LoggedIn;
        [SerializeField] private GameEvent _paintingAnimationTrigger;
        
        private void Start() {
            fusePlugged = _fusePlugged;
            LoggedIn = _LoggedIn;
            paintingAnimationTrigger = _paintingAnimationTrigger;
        }
    }
}