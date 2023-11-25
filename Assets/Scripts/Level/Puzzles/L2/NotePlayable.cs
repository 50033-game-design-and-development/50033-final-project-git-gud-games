using UnityEngine;

public class NotePlayable : MonoBehaviour, IInteractable, IClickable {
    public Music.Note note;

    private Music.Octave _octave;

    public void OnInteraction() {
        Debug.Log("Played Note: ("+note+", "+_octave+")");
        // todo: Synthesize note sounds
        Event.L2.playNote.Raise(note, _octave);
    }

    public void OnClick() => OnInteraction();

    private void Start() {
        _octave = transform.parent.gameObject.GetComponent<Octave>().octave;
    }
}
