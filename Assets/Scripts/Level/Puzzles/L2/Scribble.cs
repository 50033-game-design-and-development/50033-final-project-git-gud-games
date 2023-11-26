using UnityEngine;

public class Scribble : MonoBehaviour {
    private GameObject scribble;

    public void Visible(bool state) {
        scribble.SetActive(!state);
    }

    private void Start() {
        scribble = transform.GetChild(0).gameObject;
    }
}
