using UnityEngine;

public class NotePlayable : MonoBehaviour, IInteractable, IClickable {
    public Music.Note note;

    private Music.Octave _octave;
    private AudioSource _pianoSrc;

    public void OnInteraction() {
        Debug.Log("Played Note: ("+note+", "+_octave+")");
        // todo: Synthesize note sounds
        Event.L2.playNote.Raise(note, _octave);
    }

    public void OnClick() => OnInteraction();

    private void Start() {
        var oct = transform.parent.gameObject;
        _octave = oct.GetComponent<Octave>().octave;
        _pianoSrc = oct.transform.parent.gameObject.GetComponent<AudioSource>();
    }
}
