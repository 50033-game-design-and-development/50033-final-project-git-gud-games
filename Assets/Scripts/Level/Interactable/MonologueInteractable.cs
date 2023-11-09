using UnityEngine;

public class MonologueInteractable : MonoBehaviour, IInteractable {
    public void OnInteraction() {
        Debug.Log("Setting monologue for " + gameObject.name);
    }
}