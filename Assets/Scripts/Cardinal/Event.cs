using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;
    public static IntGameEvent showDialogue;
    public static GameEvent testEvent; // TODO: to be removed

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;
    public IntGameEvent _showDialogue;
    public GameEvent _testEvent; // TODO: to be removed


    void Start() {
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
        showDialogue = _showDialogue;
        // Used to test incrementing state of SFX and monologue
        testEvent = _testEvent; // TODO: to be removed
    }
}
