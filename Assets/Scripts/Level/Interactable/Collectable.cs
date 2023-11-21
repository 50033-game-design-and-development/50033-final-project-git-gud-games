using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public Sprite invSprite;
    public InventoryItem itemType;
    public GameEvent @event;
    [SerializeField] private bool canCollect;

    private Inv.Collectable invItem => new() {
        itemType = itemType,
        itemSprite = invSprite,
    };

    public void OnInteraction() {
        if (!canCollect) {
            return;
        }

        Debug.Log("Collect " + gameObject.name);
        GameState.inventory.Add(invItem);
        Event.Global.inventoryUpdate.Raise();

        float duration = 0f;
        if (TryGetComponent(out SFXInteractable sfxInteractable)) {
            duration = sfxInteractable.GetAudioLength();
        }

        if (@event != null) {
            @event.Raise();
        }

        StartCoroutine("Collect", duration);
    }

    public void SetCanCollect(bool value) {
        canCollect = value;
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
