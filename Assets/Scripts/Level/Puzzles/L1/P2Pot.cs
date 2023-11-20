using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Pot : MonoBehaviour {

    // [Header("Debug")] [SerializeField]
    private HashSet<InventoryItem> potItems = new HashSet<InventoryItem>();

    [Header("Attributes")]
    [SerializeField] private Transform ingredientsTransform;
    [SerializeField] private GameEvent L1P2Solved;
    
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
        Debug.Log("Checking combination");
        // Check if chicken, banana, and capsicum are in the pot
        // If so, lock the pot
        // TODO: integrate
        InventoryItem[] items = new InventoryItem[] {
            InventoryItem.L1_Chicken,
            InventoryItem.L1_Banana,
            InventoryItem.L1_Tomato
        };

        if (potItems.Count != items.Length) 
            return;

        foreach (InventoryItem item in items) {
            if (!potItems.Contains(item)) 
                return;
        }

        Debug.Log("Combination correct");
        // TODO: integrate into Event
        L1P2Solved.Raise();
        
    }

    // Listened on L1P2Solved event
    public void LockIngredients() {
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
