using Event;
using UnityEngine;

public class PaintingInteractable : MonoBehaviour, IInteractable {
    public void OnInteraction() {
        L2.clickPainting.Raise();
    }
}
