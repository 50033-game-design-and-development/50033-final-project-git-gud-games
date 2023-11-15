using UnityEngine;

public class L0P1Paper2 : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private Collider boxCollider;
    public Transform paper1;

    public Transform min;
    public Transform max;
    private Vector3 _min;
    private Vector3 _max;

    // To be called by event listener
    public void PlacePaperOnDesk() {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<Collider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;

        _min = min.position;
        _max = max.position;
    }

    private void Update() {
        // Snap to correct position once it is close enough
        if ((transform.position - paper1.position).magnitude < 0.01f) {
            Event.L0P1SolvedEvent.Raise();
            Destroy(gameObject);
        }
        
        AdjustTransformToBounds();
        
    }

    private void AdjustTransformToBounds() {

        if (transform.position.x > _max.x) {
            transform.position = new Vector3(_max.x, transform.position.y, transform.position.z);
        } 
        else if (transform.position.x < _min.x) {
            transform.position = new Vector3(_min.x, transform.position.y, transform.position.z);
        }

        if (transform.position.z > _max.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _max.z);
        } 
        else if (transform.position.z < _min.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _min.z);
        }

    }
}
