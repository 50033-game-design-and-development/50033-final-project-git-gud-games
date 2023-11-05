using System.Linq;
using UnityEngine;

public class CollectibleInteractable : MonoBehaviour, IInteractable {
    public int itemIndex;
    // public IntGameEvent onItemCollected;

    public void Interact() {
        Debug.Log("Collect " + gameObject.name);
        Destroy(gameObject);
    }
}