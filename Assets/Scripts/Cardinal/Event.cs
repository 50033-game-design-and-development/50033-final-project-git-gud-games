using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;
    public static IntGameEvent showDialogue;
    public static GameEvent L0P1SolvedEvent;
    public static GameEvent itemPlaced;

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;
    public IntGameEvent _showDialogue;
    public GameEvent _L0P1SolvedEvent;
    public GameEvent _itemPlaced;


    void Start() {
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
        showDialogue = _showDialogue;
        L0P1SolvedEvent = _L0P1SolvedEvent;
        itemPlaced = _itemPlaced;
    }
}
