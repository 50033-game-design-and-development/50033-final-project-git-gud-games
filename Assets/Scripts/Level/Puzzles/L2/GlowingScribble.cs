using UnityEngine;

public class GlowingScribble: MonoBehaviour {
    public void OnLightsToggle(bool turnOn) {
        GetComponent<MeshRenderer>().enabled = !turnOn;
    }
}

