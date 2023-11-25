using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static GameEvent clickPainting;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solvedP4;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solvedP4;

        private void Start() {
            clickPainting = _clickPainting;
            playNote = _playNote;
            plugFuse = _plugFuse;
            solvedP4 = _solvedP4;
        }
    }
}