using UnityEngine;

/// <summary>
/// This script is used to control the view bobbing animation of the player.
/// It is attached to the player object, but possible to refactor in the future
/// to call animations in event style.
/// </summary>
public class PlayerAnimationController : MonoBehaviour {
    public Animator viewAnimator;
    public CharacterController controller;
    private static readonly int SPEED = Animator.StringToHash("Speed");

    private void SetSpeed() {
        // Get planar horizontal velocity of the controller
        Vector3 velocity = controller.velocity;
        velocity.y = 0;
        viewAnimator.SetFloat(SPEED, velocity.magnitude);
    }

    private void Update() {
        SetSpeed();
    }
}