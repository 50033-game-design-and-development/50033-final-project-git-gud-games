using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {
        public static NoteOctaveGameEvent playNote;
        public static GameEvent solvedP4;

        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _solvedP4;

        private void Start() {
            playNote = _playNote;
            solvedP4 = _solvedP4;
        }
    }
}
