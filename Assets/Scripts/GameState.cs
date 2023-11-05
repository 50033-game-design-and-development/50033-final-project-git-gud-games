using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryItems {
    Ball = 1,
    Paper = 2
}

public class GameState : MonoBehaviour {
    // Start is called before the first frame update
    private static List<InventoryItems> _inventory = new List<InventoryItems>();

    public static bool AddInventoryItem(InventoryItems item) {
        if (_inventory.Contains(item)) { return false; }
        _inventory.Add(item);
        return true;
    }

    public static bool RemoveInventoryItem(InventoryItems item) {
        return _inventory.Remove(item);
    }

    public static bool HasInventoryItem(InventoryItems item) {
        return _inventory.Contains(item);
    }
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
