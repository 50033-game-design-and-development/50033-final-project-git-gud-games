using UnityEngine;

public class StudyRoomLightInteractable : LightInteractable {
    private bool _fuseInserted;

    public void OnFuseInsert() {
        _fuseInserted = true;
        base.ApplyLighting();
    }

    public override void OnInteraction() {
        if (_fuseInserted) {
            base.OnInteraction();
        } else {
            base.ToggleSwitch();
        }
    }

    protected override void Start() {
        bool initialState = base.switchedOn;
        base.switchedOn = false;
        base.ApplyLighting();
        base.switchedOn = initialState;
    }
}
