using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static GameEvent clickPainting;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solveP3;
        public static GameEvent solvedP4;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solveP3;
        [SerializeField] private GameEvent _solvedP4;

        private void Start() {
            clickPainting = _clickPainting;
            playNote = _playNote;
            plugFuse = _plugFuse;
            solveP3 = _solveP3;
            solvedP4 = _solvedP4;
        }
    }
}