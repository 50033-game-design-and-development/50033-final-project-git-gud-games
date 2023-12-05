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

    private void PlayCutscene() {
        if (GameState.isInventoryOpened) {
            GameState.ToggleInventory();
        }

        GameState.isCutscenePlaying = true;
        _selfPlayingCutscene = true;

        _director.Play();

        StartCoroutine(SetCutscenePlaying());
    }

    private void Start() {
        _director = GetComponent<PlayableDirector>();
    }

    private IEnumerator SetCutscenePlaying() {
        while (_director.state == PlayState.Playing) {
            yield return null;
        }

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
    }

    private void Update() {
        if (_director.state == PlayState.Playing && !_selfPlayingCutscene) {
            _selfPlayingCutscene = true;
            GameState.isCutscenePlaying = true;
            if (GameState.isInventoryOpened) {
                GameState.ToggleInventory();
            }
            StartCoroutine(SetCutscenePlaying());
        }
    }

    private void OnDisable() {
        if(!_selfPlayingCutscene) return;

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
    }
}
