using UnityEngine;

public class GlowingScribble: MonoBehaviour {
    public void OnLightsToggle(bool lightsOn) {
        GetComponent<MeshRenderer>().enabled = !lightsOn;
    }
}

