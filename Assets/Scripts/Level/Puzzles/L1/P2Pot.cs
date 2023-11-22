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
    private int clickState = 0;
    private bool canCook;
    private MeshCollider potCollider;

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

    public void RemoveIngredient(InventoryItem item) {
        if (potItems.Contains(item)) {
            potItems.Remove(item);
        }

        CheckCombination();
    }

    public void OnStewClicked() {
        if (!solved || clickState > 4)
            return;
        
        clickState ++;
        if (clickState == 5)
            Event.L1.drinkStew.Raise();
        else if (clickState < 5) {
            MonologueInteractable monologueInteractable = GetComponent<MonologueInteractable>();
            // monologueInteractable.OnInteraction();
            monologueInteractable.IncrementState();
        }
    }

    public void OnStewDrink() {
        Debug.Log("GANPEIIIII");
        // TODO: Play some cutscene
    }

    public void SetCanCook(bool value) {
        canCook = value;
        potCollider.enabled = true;
    }

    public void ToggleCollider() {
        if (!canCook) {
            potCollider.enabled = !GameState.isInventoryOpened;
        }
    }

    private void LockIngredients() {
        solved = true;
        GetComponent<BoxCollider>().enabled = true;
        Destroy(GetComponent<DragDoppable>());
    }

    private void Start() {
        potCollider = GetComponent<MeshCollider>();
    }
}
