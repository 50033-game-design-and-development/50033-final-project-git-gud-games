using UnityEngine;

namespace Event {
    public class Global : MonoBehaviour {
        public static GameEvent hideAll;
        public static GameEvent revealAll;
        public static Vector3GameEvent revealPositionOnUi;
        public static MonologueKeyGameEvent showDialogue;
        public static GameEvent inventoryUpdate;

        public GameEvent _hideAll;
        public GameEvent _revealAll;
        public Vector3GameEvent _revealPositionOnUi;
        public MonologueKeyGameEvent _showDialogue;
        public GameEvent _inventoryUpdate;

        void Start() {
            revealAll = _revealAll;
            hideAll = _hideAll;
            revealPositionOnUi = _revealPositionOnUi;
            showDialogue = _showDialogue;
            inventoryUpdate = _inventoryUpdate;
        }
    }
}
