using UnityEngine;

public class InventoryItemInteractionTrigger : MonoBehaviour {
    [SerializeField] private InventoryItem type;

    public void TriggerInteraction(InventoryItem targetItem) {
        if (!targetItem.Equals(type)) 
            return; 

        foreach (IInteractable interactable in GetComponents<IInteractable>()) {
            interactable.OnInteraction();
        }

    }
}