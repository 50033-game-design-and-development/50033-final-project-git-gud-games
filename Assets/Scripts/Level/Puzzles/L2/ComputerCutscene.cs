using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ComputerCutscene : MonoBehaviour, IInteractable {
    private int state = 0;

    [SerializeField] private int playState = 1;

    private bool _selfPlayingCutscene;

    [SerializeField] public PlayableDirector startingCutscene;
    [SerializeField] public PlayableDirector endingCutscene;

    private float raycastDist;

    public void OnInteraction() {
        if (_selfPlayingCutscene)
            return;

        if (state == playState) {
            PlayCutscene();
            IncrementState();

        }
    }

    public void OnCloseAudioFile(MonologueKey key) {
        if (key != MonologueKey.L2_PC_AUDIO) {
            return;
        }
        EndCutscene();
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

        startingCutscene.Play();
        GameState.isCutscenePlaying = true;
        _selfPlayingCutscene = true;
        GameState.LockCursor();
        
        raycastDist = GameState.raycastDist;
        GameState.raycastDist = 0;
    }


    public void EndCutscene() {
        if (!_selfPlayingCutscene)
            return;
        
        endingCutscene.Play();
        StartCoroutine(EndCutsceneCoroutine());
        
    }

    private IEnumerator EndCutsceneCoroutine() {
        while (endingCutscene.state == PlayState.Playing) {
            yield return null;
        }

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
        GameState.ConfineCursor();
        GameState.raycastDist = raycastDist;
    }


    private void OnDisable() {
        if(!_selfPlayingCutscene) return;

        GameState.isCutscenePlaying = false;
        _selfPlayingCutscene = false;
    }


}
