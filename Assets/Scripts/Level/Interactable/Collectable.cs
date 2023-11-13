using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable {
    public int itemIndex;
    // public IntGameEvent onItemCollected;

    // TODO:
    public void OnInteraction() {
        Debug.Log("Collect " + gameObject.name);
        float duration = GetComponent<SFXInteractable>().audioClips[0].length;
        StartCoroutine("Collect", duration);
    }

    private IEnumerator Collect(float duration) {
        // Disable model and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        // To test incrementing state, TODO: remove
        Event.L0P1SolvedEvent.Raise();

        // Play out the SFX before destroying
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
