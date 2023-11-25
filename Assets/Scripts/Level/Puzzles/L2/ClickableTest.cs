using UnityEngine;

public class ClickableTest: MonoBehaviour {
    public GameEvent clickEvent;
    
    public void OnClick(bool value) {
        if (value) { return; }
        Debug.Log("TESTING");
        clickEvent.Raise();
    }
}

