using UnityEngine;

public class DestroyAfterRitual : MonoBehaviour {
    public void OnRitualComplete() {
        Destroy(gameObject);
    }
}
