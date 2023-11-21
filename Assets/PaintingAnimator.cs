using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAnimator : MonoBehaviour
{
    public Animator paintingAnimator;

    public void playAnimation()
    {
        paintingAnimator.Play("Rotate Painting");
    } 
}
