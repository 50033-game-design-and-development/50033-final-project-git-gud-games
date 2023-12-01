using UnityEngine;

public class FallingPicture : MonoBehaviour {
    public void Animate(MonologueKey key) {
        if (key == MonologueKey.L2_PICTURE_LOOSE) {
            Event.L2.clickPainting.Raise();
        }
    }
}
