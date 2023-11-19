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
            }
            renderer.materials = materials.ToArray();
        }
    }

    private void Awake() {
        _renderers = GetComponentsInChildren<Renderer>();
    }
}
