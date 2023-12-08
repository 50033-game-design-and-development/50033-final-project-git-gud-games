using System.Collections.Generic;
using UnityEngine;

public class P2Pot : MonoBehaviour, IInteractable {

    private readonly HashSet<InventoryItem> _potItems = new();

    [Header("Attributes")]
    [SerializeField] private Transform ingredientsTransform;
    [SerializeField] private Collectable vialFilled;

    [Header("Password")]
    [SerializeField] private InventoryItem[] correctCombination;
    
    [Header("Ingredient details")]    
    [SerializeField] private List<InventoryItem> validItems = new();
    [SerializeField] private List<GameObject> itemPrefabs = new();

    private bool _solved;

    public void AddIngredient() {
        if (!GameState.selectedInventoryItem.HasValue) 
            return;

        InventoryItem item = GameState.selectedInventoryItem.Value.itemType;
        bool isIngredient = validItems.Contains(item);
        if (!isIngredient) {
            if (_solved && item == InventoryItem.L1_Vial) {
                    vialFilled.OnInteraction();
            }
            return;
        }

        _potItems.Add(item);
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
        if (_potItems.Count != correctCombination.Length)
            return;

        // Can be optimized to set
        foreach (InventoryItem item in correctCombination) {
            if (!_potItems.Contains(item))
                return;
        }

        Event.L1.solveP2.Raise();
    }

    public void OnP2Solved() {
        _solved = true;
        LockIngredients();
        // TODO: play sinking animation (?)
    }

    public void RemoveIngredient(InventoryItem item) {
        if (_potItems.Contains(item)) {
            _potItems.Remove(item);
        }

        CheckCombination();
    }

    public void OnInteraction() {
        /*
        if (!_solved || _clickState > 5)
            return;
        
        _clickState ++;
        if (_clickState == 5)
            Event.L1.drinkStew.Raise();
        else if (_clickState < 5) {
            MonologueInteractable monologueInteractable = GetComponent<MonologueInteractable>();
            // monologueInteractable.OnInteraction();
            monologueInteractable.IncrementState();
        }
        */
    }

    public void OnStewDrink() {
        // Play some cutscene
    }

    private void LockIngredients() {
        _solved = true;
        GetComponent<BoxCollider>().enabled = true;
        DragDoppable droppable = GetComponent<DragDoppable>();
        droppable.possibleDroppable.Clear();
        droppable.possibleDroppable.Add(InventoryItem.L1_Vial);
        droppable.UpdateDroppables();
    }
}
