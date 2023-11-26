using UnityEngine;

public class Lamp : MonoBehaviour {
    private MeshRenderer mesh;
    private Light lightSource;
    [SerializeField] private Material lampsOnMaterial;
    [SerializeField] private Material lampsOffMaterial;

    public void Switch(bool state) {
        mesh.material = state ? lampsOnMaterial : lampsOffMaterial;
        lightSource.enabled = state;
    }

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        lightSource = GetComponentInChildren<Light>();
    }
}
