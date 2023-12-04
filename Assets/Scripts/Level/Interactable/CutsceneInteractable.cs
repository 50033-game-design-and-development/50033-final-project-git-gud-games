using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInteractable : MonoBehaviour, IInteractable {
    private int state = 0;

    [SerializeField] private int playState = 1;

    public void OnInteraction() {
        if (state == playState) {
            PlayCutscene();
            IncrementState();
        }
    }

    private void PlayCutscene() {
        // TODO: Lock player input

        var director = GetComponent<PlayableDirector>();
        director.Play();

        // while (director.state == PlayState.Playing);
        // TODO: Unlock player input
    }

    public void IncrementState() {
        state++;
    }

    public void ToggleInventory() {
        GameState.ToggleInventory();
    }
}
