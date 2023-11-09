using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent hideAll;
    public static GameEvent revealAll;
    public static Vector3GameEvent revealPositionOnUi;

    public GameEvent _hideAll;
    public GameEvent _revealAll;
    public Vector3GameEvent _revealPositionOnUi;
    void Start() {
        revealAll = _revealAll;
        hideAll = _hideAll;
        revealPositionOnUi = _revealPositionOnUi;
    }
}
