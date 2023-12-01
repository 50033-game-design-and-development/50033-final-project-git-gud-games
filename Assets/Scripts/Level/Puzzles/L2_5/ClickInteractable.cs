using System.Collections;
using UnityEngine;
public class ClickInteractable: Clickable, IInteractable {
    public void OnInteraction() => OnClick();

    public override void OnClick() {
        if(!canClick) return;
        if(@event != null) {
            @event.Raise();
        }

        if(!destroyOnClick) return;
        
        float duration = 0f;
        if (TryGetComponent(out SFXClickable sfxClickable)) {
            duration = sfxClickable.GetAudioLength();
        }
        StartCoroutine("Destroy", duration);
    }
    
    private IEnumerator Destroy(float duration) {
        // Disable model and collider
        if (TryGetComponent(out MeshRenderer meshRenderer)) {
            meshRenderer.enabled = false;
        }
        if (TryGetComponent(out Collider collider)) {
            collider.enabled = false;
        }
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        // Play out the SFX before destroying
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}