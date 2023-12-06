using UnityEngine;

namespace Event {
    public class L2 : MonoBehaviour {

        public static GameEvent finishRecording;
        public static GameEvent clickPainting;
        public static GameEvent insertFloppy;
        public static NoteOctaveGameEvent playNote;
        public static GameEvent plugFuse;
        public static GameEvent solveP3;
        public static GameEvent solvedP4;
        public static GameEvent solvedP6;
        public static GameEvent placeAllCandles;
        public static GameEvent enterLivingRoom;
        public static GameEvent enterSecretRoom;
        public static BoolGameEvent onPasswordScreen;
        public static BoolGameEvent secretRoomRevealable;
        public static BoolGameEvent livingRoomRevealable;
        public static BoolGameEvent studyRoomRevealable;
        public static GameEvent shadowDespawned;

        [SerializeField] private GameEvent _clickPainting;
        [SerializeField] private GameEvent _finishRecording;
        [SerializeField] private GameEvent _insertFloppy;
        [SerializeField] private NoteOctaveGameEvent _playNote;
        [SerializeField] private GameEvent _plugFuse;
        [SerializeField] private GameEvent _solveP3;
        [SerializeField] private GameEvent _solvedP4;
        [SerializeField] private GameEvent _solvedP6;
        [SerializeField] private GameEvent _placeAllCandles;
        [SerializeField] private GameEvent _enterLivingRoom;
        [SerializeField] private GameEvent _enterSecretRoom;
        [SerializeField] private BoolGameEvent _onPasswordScreen;
        [SerializeField] private BoolGameEvent _secretRoomRevealable;
        [SerializeField] private BoolGameEvent _livingRoomRevealable;
        [SerializeField] private BoolGameEvent _studyRoomRevealable;
        [SerializeField] private GameEvent _shadowDespawned;


        private void Start() {
            finishRecording = _finishRecording;
            clickPainting = _clickPainting;
            insertFloppy = _insertFloppy;
            playNote = _playNote;
            plugFuse = _plugFuse;
            solveP3 = _solveP3;
            solvedP4 = _solvedP4;
            solvedP6 = _solvedP6;
            placeAllCandles = _placeAllCandles;
            enterLivingRoom = _enterLivingRoom;
            enterSecretRoom = _enterSecretRoom;
            onPasswordScreen = _onPasswordScreen;
            secretRoomRevealable = _secretRoomRevealable;
            livingRoomRevealable = _livingRoomRevealable;
            studyRoomRevealable = _studyRoomRevealable;
            shadowDespawned = _shadowDespawned;
        }
    }
}
