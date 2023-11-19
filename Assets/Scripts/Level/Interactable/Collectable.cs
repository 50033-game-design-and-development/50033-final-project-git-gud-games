using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public Sprite invSprite;
    public InventoryItem itemType;

    private Inv.Collectable invItem => new() {
        itemType = itemType,
        itemSprite = invSprite,
    };

    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);
        GameState.inventory.Add(invItem);
        Event.Global.inventoryUpdate.Raise();

        float duration = 0f;
        if (TryGetComponent(out SFXInteractable sfxInteractable)) {
            duration = sfxInteractable.audioClips[0].length;
        }
        
        StartCoroutine("Collect", duration);
    }

    private IEnumerator Collect(float duration) {
        // Disable model and collider
        if (TryGetComponent(out MeshRenderer meshRenderer)) {
            meshRenderer.enabled = false;
        }
        if (TryGetComponent(out Collider collider)) {
            collider.enabled = false;
        }

        // Play out the SFX before destroying
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
