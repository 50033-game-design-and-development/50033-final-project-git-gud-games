using System.Collections.Generic;
using UnityEngine;

public class P4NoteSequence : MonoBehaviour {
    public Music.Octave octave;
    public List<Music.Note> sequence;

    private int _idx;

    public void OnNotePlayed(Music.Note note, Music.Octave octave) {
        if (note != sequence[_idx]) {
            _idx = 0;
            return;
        }

        ++_idx;
        if (_idx != sequence.Count) {
            return;
        }

        Event.L2.solvedP4.Raise();
        Debug.Log("solved");
        enabled = false;
    }
}
