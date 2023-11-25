using System.Collections.Generic;
using UnityEngine;

public class P4Shelf : MonoBehaviour {
    public Animator animator;

    // use this to reveal all level 2.5 interactables once the bookshelf opens
    public List<Revealable> interactables = new();

    private static readonly int INTERACT = Animator.StringToHash("Interact");

    // called on L2::SolvedP4
    public void OnPuzzleSolved() {
        animator.SetTrigger(INTERACT);
        foreach(var i in interactables) {
            i.isVisible = true;
        }
    }
}
