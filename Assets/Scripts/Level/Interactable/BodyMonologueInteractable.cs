using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Monologue Interactable due to the state requirement, but
/// includes elements of a Collectable 
/// and also has SFX and UI elements (in the cutscene).
/// Is there a better way to do this?
/// Will a state machine with Enter/Exit Actions help?
/// </summary>
public class BodyMonologueInteractable : MonologueInteractable {

    [SerializeField]
    private Sprite keySprite;
    [SerializeField]
    private InventoryItems keyCollectable;
    [SerializeField]
    private CanvasGroup UIFadeOverlay;
    [SerializeField]
    private float uiFadeOutTime;
    [SerializeField]
    private GameEvent onInventoryUpdate;
    private AudioSource bodyAudio;
    
    private Inv.Collectable invItem {
        get => new Inv.Collectable {
            itemType = keyCollectable, 
            itemSprite = keySprite
        };
    }

    public override void OnInteraction() {
        base.OnInteraction();
        if (state == 1) {
            PlayCutscene();
            GameState.inventory.Add(invItem);
            state = 2;            
        }
    }

    private void PlayCutscene() {
        // Lock controls (not priority)
        StartCoroutine(FadeOutIn());
        bodyAudio.PlayOneShot(bodyAudio.clip);
    }

    private IEnumerator FadeOutIn() {
        UIFadeOverlay.alpha = 0;
        float inc =  uiFadeOutTime / 100;
        for (float alpha = 0; alpha < uiFadeOutTime; alpha += inc) {
            UIFadeOverlay.alpha = alpha;
            yield return new WaitForSecondsRealtime(inc);
        }

        yield return new WaitForSecondsRealtime(1f);
        onInventoryUpdate.Raise();

        for (float alpha = 1; alpha > 0; alpha -= inc) {
            UIFadeOverlay.alpha = alpha;
            yield return new WaitForSecondsRealtime(inc);
        }
        
    }

    private void Start() {
        bodyAudio = GetComponent<AudioSource>();
    }

}
