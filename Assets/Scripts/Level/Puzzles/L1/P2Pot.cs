using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Pot : MonoBehaviour {

    private HashSet<InventoryItem> potItems = new HashSet<InventoryItem>();

    [Header("Attributes")]
    [SerializeField] private Transform ingredientsTransform;
    [SerializeField] private InventoryItem[] correctCombination;
    
    [Header("Ingredient details")]    
    [SerializeField] private List<InventoryItem> validItems = new List<InventoryItem>();
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    

    private bool solved = false;
    
    public void AddIngredient() {
        if (!GameState.selectedInventoryItem.HasValue || solved) 
            return;

        InventoryItem item = GameState.selectedInventoryItem.Value.itemType;
        bool isIngredient = validItems.Contains(item);
        if (!isIngredient) 
            return;

        potItems.Add(item);
        InstantiatePrefab(validItems.IndexOf(item));

        CheckCombination();
        
    }

    public void InstantiatePrefab(int index) {
        GameObject prefab = itemPrefabs[index];

        // Spawn prefab at ingredientTransform as child of this
        GameObject ingredient = Instantiate(prefab, ingredientsTransform);
        ingredient.transform.parent = ingredientsTransform;
        
    }


    // Listening to potCombinationCheck event
    public void CheckCombination() {

        if (potItems.Count != correctCombination.Length) 
            return;

        // Can be optimized to set
        foreach (InventoryItem item in correctCombination) {
            if (!potItems.Contains(item)) 
                return;
        }

        Debug.Log("Combination correct");
        Event.L1.solveP2.Raise();
        
    }

    public void OnP2Solved() {
        solved = true;
        LockIngredients();
        // TODO: play sinking animation (?)
    }

    private void LockIngredients() {
        solved = true;
        GetComponent<BoxCollider>().enabled = true;
        Destroy(GetComponent<DragDoppable>());
    }

    public void RemoveIngredient(InventoryItem item) {
        if (potItems.Contains(item)) {
            potItems.Remove(item);
        }

        CheckCombination();
            
    }


}
