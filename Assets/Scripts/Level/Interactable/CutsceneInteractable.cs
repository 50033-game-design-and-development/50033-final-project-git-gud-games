using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInteractable : MonoBehaviour, IInteractable {
    private int state = 0;

    [SerializeField] private int playState = 1;


    private PlayableDirector _director;
    private bool _selfPlayingCutscene;

    public void OnInteraction() {
        if (state == playState) {
            PlayCutscene();
            IncrementState();
        }
    }

    public void IncrementState() {
        state++;
    }

    public void ToggleInventory() {
        GameState.ToggleInventory();
    }

    private void OnDisable() {
        if(!_selfPlayingCutscene) return;

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
    }

    private IEnumerator SetCutscenePlaying() {
        while (_director.state == PlayState.Playing) {
            yield return null;
        }

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
    }

    private void PlayCutscene() {
        GameState.isCutscenePlaying = true;
        _selfPlayingCutscene = true;

        _director = GetComponent<PlayableDirector>();
        _director.Play();

        StartCoroutine(SetCutscenePlaying());
    }
}
