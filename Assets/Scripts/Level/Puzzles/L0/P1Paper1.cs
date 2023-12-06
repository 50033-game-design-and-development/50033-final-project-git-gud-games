using UnityEngine;

public class P1Paper1 : MonoBehaviour {
    [SerializeField] private Material combinedPapersMaterial;

    // To be called by event listener
    public void L0P1Solved() {
        GetComponent<MeshRenderer>().material = combinedPapersMaterial;
    }
}
