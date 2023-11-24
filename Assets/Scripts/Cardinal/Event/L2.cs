using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static GameEvent clickPainting;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solveP3;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solveP3;

        private void Start() {
            clickPainting = _clickPainting;
            playNote = _playNote;
            plugFuse = _plugFuse;
            solveP3 = _solveP3;
        }
    }
}