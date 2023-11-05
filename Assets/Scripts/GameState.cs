using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryItems {
    Ball = 1,
    Paper = 2
}

public class GameState: MonoBehaviour {
    public static readonly List<Collectable> Inventory = new List<Collectable>{};
    // initial list of items to assign to inventory (for testing only)
    public List<Collectable> startInventory = new List<Collectable>();
    
    void Start() {
        foreach (var collectable in startInventory) {
            Inventory.Add(collectable);
        }
    }
    
    // Update is called once per frame
    void Update() {
        
    }
}
