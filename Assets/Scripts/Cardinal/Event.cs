using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;
    public static MonologueKeyGameEvent showDialogue;
    public static GameEvent L0P1SolvedEvent;
    public static GameEvent itemPlaced;
    public static GameEvent L0UnlockDoorEvent;
    public static GameEvent onInventoryUpdate;

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;
    public MonologueKeyGameEvent _showDialogue;
    public GameEvent _L0P1SolvedEvent;
    public GameEvent _itemPlaced;
    public GameEvent _L0UnlockDoorEvent;
    public GameEvent _onInventoryUpdate;


    void Start() {
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
        showDialogue = _showDialogue;
        L0P1SolvedEvent = _L0P1SolvedEvent;
        itemPlaced = _itemPlaced;
        L0UnlockDoorEvent = _L0UnlockDoorEvent;
        onInventoryUpdate = _onInventoryUpdate;
    }
    
}
