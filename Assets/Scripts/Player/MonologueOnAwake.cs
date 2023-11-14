using UnityEngine;

public class MonologueOnAwake : MonoBehaviour
{
    public MonologueKey monologueKey;

    private void Start() {
        GetComponent<MonologueUI>().StartMonologue((int)monologueKey);
    }
}
