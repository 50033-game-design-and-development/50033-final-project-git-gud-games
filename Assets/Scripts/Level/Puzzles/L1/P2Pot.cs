using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Pot : MonoBehaviour {

    [Header("Debug")]
    public List<InventoryItem> potItems = new List<InventoryItem>();

    [Header("Attributes")]
    [SerializeField] private Transform ingredientsTransform;
    
    [Header("Ingredient details")]    
    [SerializeField] private List<InventoryItem> validItems = new List<InventoryItem>();
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    

    

    public void OnDragDrop() {
        if (!GameState.selectedInventoryItem.HasValue) 
            return;

        InventoryItem item = GameState.selectedInventoryItem.Value.itemType;
        bool isIngredient = validItems.Contains(item);
        if (!isIngredient) 
            return;

        potItems.Add(item);
        InstantiatePrefab(validItems.IndexOf(item));
        
    }

    public void InstantiatePrefab(int index) {
        GameObject prefab = itemPrefabs[index];

        // Spawn prefab at ingredientTransform as child of this
        GameObject ingredient = Instantiate(prefab, ingredientsTransform);
        ingredient.transform.parent = ingredientsTransform;
        
    }


    // Listening to potCombinationCheck event
    public void CheckCombination() {
        Debug.LogWarning("Unimplemented");
    }


}
