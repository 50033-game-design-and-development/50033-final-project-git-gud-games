using UnityEngine;

public class P1Paper2 : MonoBehaviour {
    [SerializeField] private Transform paper1;
    [SerializeField] private Transform min;
    [SerializeField] private Transform max;
    private MeshRenderer meshRenderer;
    private Collider boxCollider;
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
            Event.L0.solveP1.Raise();
            Destroy(gameObject);
        }
        AdjustTransformToBounds();
    }

    private void AdjustTransformToBounds() {
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, _min.x, _max.x);
        newPos.z = Mathf.Clamp(newPos.z, _min.z, _max.z);

        transform.position = newPos;

    }
}
