using UnityEngine;

public class Clickable : MonoBehaviour, IClickable {
    public GameEvent @event;
    public void OnClick() {
        @event.Raise();
    }
}
