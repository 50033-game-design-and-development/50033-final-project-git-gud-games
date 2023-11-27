using System.Collections.Generic;
using UnityEngine;

public class MonologueMap : MonoBehaviour {
    private static Dictionary<MonologueKey, Monologue> monologueMap = new Dictionary<MonologueKey, Monologue>();
    private static Dictionary<MonologueKey, bool> monologueTracker = new Dictionary<MonologueKey, bool>();
    [SerializeField] private MonologueList _monologueList;

    public static Monologue Get(MonologueKey key) {
        return monologueMap[key];
    }

    public static bool HasPlayed(MonologueKey key) {
        return monologueTracker[key];
    }

    public static void SetTracker(MonologueKey key) {
        monologueTracker[key] = true;
    }

    private void Start() {
        monologueMap.Clear();
        monologueTracker.Clear();
        foreach (Monologue monologue in _monologueList.monologues) {
            monologueMap.Add(monologue.key, monologue);
            monologueTracker.Add(monologue.key, false);
        }
    }
}
