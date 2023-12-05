using System.Collections;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour {
    [SerializeField] private float chance;
    [SerializeField] private float initialDelay;
    [SerializeField] private GameObject prefab;
    [SerializeField] Transform target;
    private int colliders;
    private bool spawned;

    public void Despawned() {
        spawned = false;
        StartCoroutine("SpawnRandomly");
    }

    private IEnumerator SpawnRandomly() {
        yield return new WaitForSeconds(initialDelay);
        while (!spawned) {
            if (colliders == 0 && Random.value < chance) {
                GameObject shadow = Instantiate(prefab, transform.position, Quaternion.identity);
                shadow.SetActive(true);
                shadow.GetComponent<Shadow>().target = target;
                spawned = true;
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void Start() {
        StartCoroutine("SpawnRandomly");
    }

    private void Update() {
        transform.LookAt(target);
        Vector3 offset = -2 * new Vector3(target.forward.x, 0, target.forward.z);
        transform.position = target.position + offset;
    }

    private void OnTriggerEnter(Collider _) {
        colliders++;
    }

    private void OnTriggerExit(Collider _) {
        colliders--;
    }
}
