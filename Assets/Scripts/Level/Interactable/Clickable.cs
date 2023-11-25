using UnityEngine;

public class Clickable : MonoBehaviour, IClickable {
    public GameEvent @event;

    public bool destroyOnClick;

    public void OnClick() {
        if(@event != null) {
            @event.Raise();
        }

        if(!destroyOnClick) return;

        Destroy(gameObject);
    }
}
