using UnityEngine;

namespace Event {
    public class L1 : MonoBehaviour {
        public static GameEvent drinkStew;
        public static GameEvent solveP1;
        public static GameEvent solveP2;

        [SerializeField] private  GameEvent _drinkStew;
        [SerializeField] private  GameEvent _solveP1;
        [SerializeField] private GameEvent _solveP2;

        private void Start() {
            solveP1 = _solveP1;
            solveP2 = _solveP2;
            drinkStew = _drinkStew;
        }
    }
}
