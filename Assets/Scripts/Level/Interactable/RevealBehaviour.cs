using System.Linq;
using UnityEngine;

public class RevealBehaviour : MonoBehaviour
{

    public Vector3GameEvent revealInteractableUIEvent;
    public bool isVisible = true;

    public void OnReveal()
    {
        if (!isVisible) return;
        revealInteractableUIEvent.Raise(transform.position);
    }

}
