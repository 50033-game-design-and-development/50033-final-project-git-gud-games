﻿using UnityEngine;

namespace Event {
    public class L0 : MonoBehaviour {
        public static GameEvent collectKey;
        public static GameEvent placePaper2;
        public static GameEvent solveP1;
        public static GameEvent unlockDoor;

        public GameEvent _collectKey;
        public GameEvent _placepPaper2;
        public GameEvent _solveP1;
        public GameEvent _unlockDoor;

        private void Start() {
            collectKey = _collectKey;
            solveP1 = _solveP1;
            unlockDoor = _unlockDoor;
            placePaper2 = _placepPaper2;
        }
    }
}
