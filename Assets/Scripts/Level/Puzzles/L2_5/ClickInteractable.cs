public class ClickInteractable: Clickable, IInteractable
{
    public void OnInteraction() => OnClick();

    public new void OnClick() {
        base.OnClick();
    }
}