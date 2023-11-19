using UnityEngine;

namespace Event {
    public class L1 : MonoBehaviour {
        public static GameEvent seeBook;
        public static GameEvent solveP1;
        public static GameEvent solveP2;
        public static GameEvent drinkStew;
        public static GameEvent unlockDoor;

        public GameEvent _seeBook;
        public GameEvent _solveP1;
        public GameEvent _solveP2;
        public GameEvent _drinkStew;
        public GameEvent _unlockDoor;

        private void Start() {
            seeBook = _seeBook;
            solveP1 = _solveP1;
            solveP2 = _solveP2;
            drinkStew = _drinkStew;
            unlockDoor = _unlockDoor;
        }
    }
}
