using UnityEngine;

public class Scribble : MonoBehaviour {
    private MeshRenderer scribble;

    public void Visible(bool state) {
        scribble.enabled = !state;
    }

    private void Start() {
        scribble = GetComponent<MeshRenderer>();
    }
}
