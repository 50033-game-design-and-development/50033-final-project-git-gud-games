using UnityEngine;

public class Revealer : MonoBehaviour, IInteractable {
    [Header("Parameters")] [SerializeField]
    private Revealable revealedObject;

    [Header("State Tracking")] [SerializeField]
    private bool isRevealed = false;

    public void OnInteraction() {
        isRevealed = !isRevealed;
        revealedObject.isVisible = isRevealed;
    }
}