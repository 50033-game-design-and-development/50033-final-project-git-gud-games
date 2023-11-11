using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;
    public static IntGameEvent showDialogue;

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;
    public IntGameEvent _showDialogue;


    void Start() {
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
        showDialogue = _showDialogue;
    }
}
