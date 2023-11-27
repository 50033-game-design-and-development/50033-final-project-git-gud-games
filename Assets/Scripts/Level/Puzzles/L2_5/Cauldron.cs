using UnityEngine;

namespace Level.Puzzles.L2_5 {
    public class Cauldron: MonoBehaviour, IDragDroppable {
        public GameObject murkyBubbles;

        public void OnDragDrop() {
            Debug.Log("DROP START");
            if (!GameState.selectedInventoryItem.HasValue) {
                return;
            }

            Debug.Log("DROP START V2");
            InventoryItem item = GameState.selectedInventoryItem.Value.itemType;
            Debug.Log("ITEM " + item);
            
            if (item == InventoryItem.L1_Lily) {
                murkyBubbles.SetActive(false);
            }        
        }
    }
}
