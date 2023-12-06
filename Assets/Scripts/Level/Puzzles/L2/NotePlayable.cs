using Music;
using UnityEngine;

public class NotePlayable : MonoBehaviour, IInteractable, IClickable {
    public Note note;

    private Octave _octave;
    private AudioSource _audioSrc;
    private Animator _animator;
    private static readonly int PLAYED = Animator.StringToHash("played");

    public void OnInteraction() {
        Debug.Log("Played Note: ("+note+", "+_octave.octave+")");

        _animator.SetTrigger(PLAYED);
        _audioSrc.PlayOneShot(_octave.noteClips[note]);

        Event.L2.playNote.Raise(note, _octave.octave);
    }

    public void OnClick() => OnInteraction();

    private void Start() {
        _octave = transform.parent.GetComponent<Octave>();
        _audioSrc = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
}
