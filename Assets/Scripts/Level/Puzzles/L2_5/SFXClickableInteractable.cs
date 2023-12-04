public class SFXClickableInteractable : SFXClickable, IInteractable {
    public void OnInteraction() => OnClick();
}
