using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public int itemIndex;
    // public IntGameEvent onItemCollected;

    // TODO:
    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);
        Destroy(gameObject);
    }
}
