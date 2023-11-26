using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent clickPainting;
        public static GameEvent loggedIn;
        public static GameEvent paintingAnimationTrigger;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solveP3;
        public static GameEvent solvedP4;
        public static GameEvent seeFuseBox;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private GameEvent _loggedIn;
        [SerializeField] private GameEvent _paintingAnimationTrigger;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solveP3;
        [SerializeField] private GameEvent _solvedP4;
        [SerializeField] private GameEvent _seeFuseBox;

        private void Start() {
            clickPainting = _clickPainting;
            playNote = _playNote;
            plugFuse = _plugFuse;
            loggedIn = _loggedIn;
            paintingAnimationTrigger = _paintingAnimationTrigger;
            solveP3 = _solveP3;
            solvedP4 = _solvedP4;
            seeFuseBox = _seeFuseBox;
        }
    }
}
