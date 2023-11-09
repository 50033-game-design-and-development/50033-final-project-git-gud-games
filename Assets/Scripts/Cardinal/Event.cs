using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;
    public static GameEvent tabHighlight;

    public GameEvent _tabHighlight;

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;

    void Start() {
        tabHighlight = _tabHighlight;
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
    }
}