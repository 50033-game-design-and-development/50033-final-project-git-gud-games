using System.Collections.Generic;
using UnityEngine;

public class P4NoteSequence : MonoBehaviour {
    public List<Music.Note> sequence;

    private Music.Octave _octave;
    private int _idx;

    public void OnNotePlayed(Music.Note note, Music.Octave octave) {
        if (_idx == sequence.Count) {
            return;
        }

        if (_idx == 0) {
            _octave = octave;
        }

        if (note != sequence[_idx] || _octave != octave) {
            _idx = 0;
            return;
        }

        ++_idx;
        if (_idx != sequence.Count) {
            return;
        }

        Event.L2.solvedP4.Raise();
        Debug.Log("solved p4");
    }
}
