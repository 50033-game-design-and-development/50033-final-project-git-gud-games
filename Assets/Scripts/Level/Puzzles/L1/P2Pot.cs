using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Pot : MonoBehaviour {

    [Header("Debug")]
    public List<InventoryItem> potItems = new List<InventoryItem>();

    [Header("Item details")]    
    [SerializeField] private List<InventoryItem> validItems = new List<InventoryItem>();
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    

    // public Transform ingredientTransform;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

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

        Debug.LogWarning("Unimplemented");
    }


    // Listening to potCombinationCheck event
    public void CheckCombination() {
        Debug.LogWarning("Unimplemented");
    }


}
