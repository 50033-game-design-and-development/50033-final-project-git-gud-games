using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to control the view bobbing animation of the player.
/// It is attached to the player object, but possible to refactor in the future
/// to call animations in event style.
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    public Animator viewAnimator;
    public CharacterController controller;

    // Update is called once per frame
    void Update()
    {
        SetSpeed();
    }

    public void SetSpeed()
    {
        // Get planar horizontal velocity of the controller
        Vector3 velocity = controller.velocity;
        velocity.y = 0;
        viewAnimator.SetFloat("Speed", velocity.magnitude);
    }
}
