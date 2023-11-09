using UnityEngine;

public class Revealable : MonoBehaviour {
    public bool isVisible = true;

    public void OnReveal() {
        if (!isVisible)
            return;
        Event.revealPositionOnUi.Raise(transform.position);
    }
}