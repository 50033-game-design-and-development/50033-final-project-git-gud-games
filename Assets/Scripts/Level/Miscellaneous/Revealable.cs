using UnityEngine;

public class Revealable : MonoBehaviour {
    public bool isVisible = true;

    public void OnReveal() {
        if (!isVisible)
            return;
        Event.Global.revealPositionOnUi.Raise(transform.position);
    }

    public void SetVisible(bool value) {
        isVisible = value;
    }
}
