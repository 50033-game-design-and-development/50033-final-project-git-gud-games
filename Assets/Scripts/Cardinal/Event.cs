using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent tabHighlight;

    public GameEvent _tabHighlight;

    private void Start() {
        tabHighlight = _tabHighlight;
    }
}