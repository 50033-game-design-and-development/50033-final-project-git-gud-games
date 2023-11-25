using System;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : MonoBehaviour, IInteractable {
    [SerializeField] protected bool switchedOn = true;
    [SerializeField] private Material lampsOnMaterial;
    [SerializeField] private Material lampsOffMaterial;
    [SerializeField] private List<MeshRenderer> scribbleMeshes;
    [SerializeField] private List<MeshRenderer> lampMeshes;
    [SerializeField] private List<Light> lights;

    public virtual void OnInteraction() {
        ToggleSwitch();
        ApplyLighting();
    }

    protected void ToggleSwitch() {
        switchedOn = !switchedOn;
    }

    protected void ApplyLighting() {
        foreach (MeshRenderer mesh in lampMeshes) {
            mesh.material = switchedOn ? lampsOnMaterial : lampsOffMaterial;
        }

        foreach (Light light in lights) {
            light.enabled = switchedOn;
        }

        foreach (MeshRenderer mesh in scribbleMeshes) {
            mesh.enabled = !switchedOn;
        }
    }

    protected virtual void Start() {
        ApplyLighting();
    }
}
