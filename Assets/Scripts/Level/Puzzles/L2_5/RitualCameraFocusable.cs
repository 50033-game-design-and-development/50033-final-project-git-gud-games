using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RitualCameraFocusable : CameraFocusable {
    [SerializeField] private PlayerConstants playerConstants;

    public override void OnInteraction() {
        base.OnInteraction();
        playerConstants.raycastDistance = 4f;
    }

    protected override void OnEscape() {
        base.OnEscape();
        playerConstants.raycastDistance = 1f;
    }
}