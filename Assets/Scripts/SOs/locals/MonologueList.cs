using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonologueList", menuName = "ScriptableObjects/locals/MonologueList")]
public class MonologueList : ScriptableObject {
    public List<Monologue> monologues;
}

[Serializable]
public struct Monologue {
    public MonologueKey key;
    public List<string> strings;
    public List<AudioClip> audios;
}
