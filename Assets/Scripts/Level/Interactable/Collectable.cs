using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public int itemIndex;
    // public IntGameEvent onItemCollected;

    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);
        Destroy(gameObject);
    }
}