using UnityEngine;

namespace Event {
    public class L0 : MonoBehaviour {
        public static GameEvent collectKey;
        public static GameEvent placePaper2;
        public static GameEvent solveP1;
        public static GameEvent unlockDoor;
        public static GameEvent seePaper1;
        public static GameEvent seePaper2;

        public GameEvent _collectKey;
        public GameEvent _placePaper2;
        public GameEvent _solveP1;
        public GameEvent _unlockDoor;
        public GameEvent _seePaper1;
        public GameEvent _seePaper2;

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
