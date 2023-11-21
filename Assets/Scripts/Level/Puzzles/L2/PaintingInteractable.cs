using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingInteractable : MonoBehaviour, IInteractable {
    public void OnInteraction() {
        Event.L2.paintingAnimationTrigger.Raise();
    }
}
