using UnityEngine;

namespace Event {
    public class L0 : MonoBehaviour {
        public static GameEvent collectKey;
        public static GameEvent placePaper2;
        public static GameEvent solveP1;
        public static GameEvent unlockDoor;
        public static GameEvent seePaper1;
        public static GameEvent seePaper2;

        [SerializeField] private GameEvent _collectKey;
        [SerializeField] private GameEvent _placePaper2;
        [SerializeField] private GameEvent _solveP1;
        [SerializeField] private GameEvent _unlockDoor;
        [SerializeField] private GameEvent _seePaper1;
        [SerializeField] private GameEvent _seePaper2;

        private void Start() {
            collectKey = _collectKey;
            solveP1 = _solveP1;
            unlockDoor = _unlockDoor;
            placePaper2 = _placePaper2;
            seePaper1 = _seePaper1;
            seePaper2 = _seePaper2;
        }
    }
}
