using UnityEngine;

public class L0P1Paper1 : MonoBehaviour {
    public Material combinedPapersMaterial;

    // To be called by event listener
    public void L0P1Solved() {
        GetComponent<MeshRenderer>().material = combinedPapersMaterial;
        // TODO: play puzzle complete fanfare?
        GetComponent<MonologueInteractable>().OnInteraction();
    }
}
