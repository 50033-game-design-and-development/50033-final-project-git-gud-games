using UnityEngine;

namespace Event {
    public class L0 : MonoBehaviour {
        public static GameEvent solveP1;
        public static GameEvent seePaper1;
        public static GameEvent seePaper2;

        [SerializeField] private GameEvent _solveP1;
        [SerializeField] private GameEvent _seePaper1;
        [SerializeField] private GameEvent _seePaper2;

        private void Start() {
            solveP1 = _solveP1;
            seePaper1 = _seePaper1;
            seePaper2 = _seePaper2;
        }
    }
}
