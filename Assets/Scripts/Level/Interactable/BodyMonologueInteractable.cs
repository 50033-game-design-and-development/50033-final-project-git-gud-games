
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Monologue Interactable due to the state requirement, but
/// includes elements of a Collectable 
/// and also has SFX and UI elements (in the cutscene).
/// Is there a better way to do this?
/// Will a state machine with Enter/Exit Actions help?
/// </summary>
public class BodyMonologueInteractable : MonoBehaviour, IInteractable {
    private int state = 0;
    public void OnInteraction() {
        if (state == 1) {
            PlayCutscene();          
        }
    }

    private void PlayCutscene() {
        PlayableDirector director = GetComponent<PlayableDirector>();
        director.Play();
    }

    public void IncrementState() {
        state++;
    }


}
