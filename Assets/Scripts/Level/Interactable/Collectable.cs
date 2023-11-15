using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public GameEvent onInventoryUpdate;
    public Sprite invSprite;
    public InventoryItems itemType;

    private Inv.Collectable invItem {
        get => new Inv.Collectable {
            itemType = itemType,
            itemSprite = invSprite,
        };
    }

    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);
        GameState.inventory.Add(invItem);
        onInventoryUpdate.Raise();
        float duration = GetComponent<SFXInteractable>().audioClips[0].length;
        StartCoroutine("Collect", duration);
    }

    private IEnumerator Collect(float duration) {
        // Disable model and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Play out the SFX before destroying
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
