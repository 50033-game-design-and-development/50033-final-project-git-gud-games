using UnityEngine;

public class Clickable : MonoBehaviour, IClickable {
    public GameEvent @event;

    public bool destroyOnClick;
    public bool canClick = true;

    public virtual void OnClick() {
        if(!canClick) return;
        if(@event != null) {
            @event.Raise();
        }

        if(!destroyOnClick) return;

        Destroy(gameObject);
    }

    public void ToggleClick(bool canClick) {
        this.canClick = canClick;
    }
}
