using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent clickPainting;
        public static GameEvent fusePlugged;
        public static GameEvent loggedIn;
        public static GameEvent paintingAnimationTrigger;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private GameEvent _fusePlugged;
        [SerializeField] private GameEvent _loggedIn;
        [SerializeField] private GameEvent _paintingAnimationTrigger;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        

        private void Start() {
            clickPainting = _clickPainting;
            playNote = _playNote;
            plugFuse = _plugFuse;
            fusePlugged = _fusePlugged;
            loggedIn = _loggedIn;
            paintingAnimationTrigger = _paintingAnimationTrigger;
        }
    }
}