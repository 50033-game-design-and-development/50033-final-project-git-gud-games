using System.Collections;
using UnityEngine;

public class Shadow : MonoBehaviour {
    public Transform target;
    [SerializeField] private float despawnDelay;

    private IEnumerator Despawn() {
        GetComponent<CameraGlitch>().Glitch(despawnDelay);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(despawnDelay);
        Event.L2.shadowDespawned.Raise();
        Destroy(gameObject);
    }

    private void Update() {
        transform.LookAt(target);
    }

    private void OnBecameVisible() {
        StartCoroutine("Despawn");
    }
}
