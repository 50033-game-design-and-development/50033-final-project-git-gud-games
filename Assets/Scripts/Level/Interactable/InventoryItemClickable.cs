using UnityEngine;

public class InventoryItemClickable : MonoBehaviour, IClickable {
    [SerializeField] private InventoryItemGameEvent @event;
    [SerializeField] private InventoryItem item;
    
    public void OnClick() {
        @event.Raise(item);
    }
    
}
