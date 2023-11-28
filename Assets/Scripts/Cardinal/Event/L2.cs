using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent finishRecording;
        public static GameEvent clickPainting;
        public static GameEvent enlargeFlames;
        public static GameEvent insertFloppy;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solveP3;
        public static GameEvent solvedP4;
        public static GameEvent seeFuseBox;
        public static GameEvent placeAllCandles;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private GameEvent _enlargeFlames;
        [SerializeField] private GameEvent _finishRecording;
        [SerializeField] private GameEvent _insertFloppy;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solveP3;
        [SerializeField] private GameEvent _solvedP4;
        [SerializeField] private GameEvent _seeFuseBox;
        [SerializeField] private GameEvent _placeAllCandles;


        private void Start() {
            finishRecording = _finishRecording;
            enlargeFlames = _enlargeFlames;
            clickPainting = _clickPainting;
            insertFloppy = _insertFloppy;
            playNote = _playNote;
            plugFuse = _plugFuse;
            solveP3 = _solveP3;
            solvedP4 = _solvedP4;
            seeFuseBox = _seeFuseBox;
            placeAllCandles = _placeAllCandles;
        }
    }
}
