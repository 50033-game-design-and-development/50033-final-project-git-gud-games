using UnityEngine;

public class PianoKeyClickable : MonoBehaviour, IClickable {
    [SerializeField] private AudioClip note;
    private AudioSource audioSource;

    public void OnClick() {
        audioSource.PlayOneShot(note);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
}
