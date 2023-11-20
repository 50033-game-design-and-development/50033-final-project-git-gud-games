using UnityEngine;

public class MonologueOnAwake : MonoBehaviour {
    [SerializeField] private MonologueKey monologueKey;

    private void Start() {
        GetComponent<MonologueUI>().StartMonologue(monologueKey);
    }
}
