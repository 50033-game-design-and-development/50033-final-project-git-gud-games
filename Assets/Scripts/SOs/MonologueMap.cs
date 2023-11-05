using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "MonologueMap", menuName = "ScriptableObjects/MonologueMap", order = 1)]
public class MonologueMap : ScriptableObject {
    public AudioClip[] audioList;
    public string[] textList;
}
