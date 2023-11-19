using System.Linq;
using UnityEngine;

public class RevealRecipe : MonoBehaviour {
    public Material blankPageMaterial;
    public Material recipePageMaterial;
    private Renderer[] _renderers;
    
    public void SwitchMaterial() {
        foreach (var renderer in _renderers) {
            // Append outline shaders
            var materials = renderer.sharedMaterials.ToList();
            if (!materials.Contains(recipePageMaterial)) {
                materials.Add(recipePageMaterial);
                materials.Remove(blankPageMaterial);
                GetComponent<AudioSource>().Play();
                Event.L1.solveP1.Raise();
            }
            renderer.materials = materials.ToArray();
        }
    }

    private void Awake() {
        _renderers = GetComponentsInChildren<Renderer>();
    }
}
