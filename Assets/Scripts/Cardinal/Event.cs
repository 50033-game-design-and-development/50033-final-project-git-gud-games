using UnityEngine;

public class Event : MonoBehaviour {
    public static GameEvent tabHighlight;

    public GameEvent _tabHighlight;
    void Start() {
        tabHighlight = _tabHighlight;
    }
}