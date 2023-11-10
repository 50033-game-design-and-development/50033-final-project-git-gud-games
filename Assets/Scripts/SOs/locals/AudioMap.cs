using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMap", menuName = "ScriptableObjects/locals/AudioMap", order = 2)]
public class AudioMap : ScriptableObject {
    public List<AudioClip> audioList;
}
