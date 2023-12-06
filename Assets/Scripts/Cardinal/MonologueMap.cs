using System.Collections.Generic;
using UnityEngine;

public class MonologueMap : MonoBehaviour {
    private static Dictionary<MonologueKey, Monologue> monologueMap = new Dictionary<MonologueKey, Monologue>();
    [SerializeField] private MonologueList _monologueList;

    public static Monologue Get(MonologueKey key) {
        return monologueMap[key];
    }

    private void Start() {
        monologueMap.Clear();
        foreach (Monologue monologue in _monologueList.monologues) {
            monologueMap.Add(monologue.key, monologue);
        }
    }
}
