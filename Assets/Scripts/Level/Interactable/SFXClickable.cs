using UnityEngine;

public class SFXClickable : SFXAbstract, IClickable {
    public void OnClick() {
        PlaySFX();
    }
}
