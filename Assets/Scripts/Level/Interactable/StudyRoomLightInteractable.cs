using UnityEngine;

public class StudyRoomLightInteractable : LightInteractable {
    private bool _fuseInserted;

    public void OnFuseInsert() {
        _fuseInserted = true;
        base.@event.Raise(base.switchedOn);
    }

    public override void OnInteraction() {
        if (_fuseInserted) {
            base.OnInteraction();
        } else {
            base.ToggleSwitch();
        }
    }

    protected override void Start() {
        base.@event.Raise(false);
    }
}
