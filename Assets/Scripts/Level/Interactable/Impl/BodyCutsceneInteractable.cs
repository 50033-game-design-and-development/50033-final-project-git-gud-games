
using UnityEngine;


public class BodyCutsceneInteractable : CutsceneInteractable {
    public override void OnInteraction() {
        if (state == 1) {
            PlayCutscene();   
            IncrementState();       
        }
    }

}
