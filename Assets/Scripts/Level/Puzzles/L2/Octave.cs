using System.Collections.Generic;
using Music;
using UnityEngine;

public class Octave : MonoBehaviour {
    public Music.Octave octave;
    public Dictionary<Note, AudioClip> noteClips;

    [SerializeField] private AudioClip C4;
    [SerializeField] private AudioClip CS4;
    [SerializeField] private AudioClip D4;
    [SerializeField] private AudioClip DS4;
    [SerializeField] private AudioClip E4;
    [SerializeField] private AudioClip F4;
    [SerializeField] private AudioClip FS4;
    [SerializeField] private AudioClip G4;
    [SerializeField] private AudioClip GS4;
    [SerializeField] private AudioClip A4;
    [SerializeField] private AudioClip AS4;
    [SerializeField] private AudioClip B4;

    private void Start() {
        noteClips = new Dictionary<Note, AudioClip>() {
           {Note.C, C4},
           {Note.CS, CS4},
           {Note.D, D4},
           {Note.DS, DS4},
           {Note.E, E4},
           {Note.F, F4},
           {Note.FS, FS4},
           {Note.G, G4},
           {Note.GS, GS4},
           {Note.A, A4},
           {Note.AS, AS4},
           {Note.B, B4},
       };
    }
}
