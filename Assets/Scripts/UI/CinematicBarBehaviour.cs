using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarBehaviour : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private string activationBool = "Activated";

    public void ActivateBar() {
        animator.SetBool("Activated", true);
    }

    public void DeactivateBar() {
        animator.SetBool("Activated", false);
    }

    private void Start() {
        animator = GetComponent<Animator>();
        DeactivateBar();
    }
}
