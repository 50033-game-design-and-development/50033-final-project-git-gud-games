using UnityEngine;

public class L0P1Paper2 : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private Collider boxCollider;
    public Transform paper1;

    // To be called by event listener
    public void PlacePaperOnDesk() {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<Collider>();
        //meshRenderer.enabled = false;
        //boxCollider.enabled = false;
    }

    private void Update() {
        // Snap to correct position once it is close enough
        if ((transform.position - paper1.position).magnitude < 0.01f) {
            Event.L0P1SolvedEvent.Raise();
            Destroy(gameObject);
        }
    }
}
