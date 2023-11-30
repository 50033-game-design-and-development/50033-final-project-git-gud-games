using UnityEngine;

namespace Level.Puzzles.L2_5 {
    public class CandlesLitEmulator: MonoBehaviour, IDragDroppable {
        public GameEvent candlesLitEvent;
        public void OnDragDrop() {
            if (!GameState.selectedInventoryItem.HasValue) {
                return;
            }
            
            var selectedInventoryItem = GameState.selectedInventoryItem.Value;
            InventoryItem itemType = selectedInventoryItem.itemType;

            if (itemType == InventoryItem.L2_5_Candles) {
                candlesLitEvent.Raise();
            }
        }
    }
}
