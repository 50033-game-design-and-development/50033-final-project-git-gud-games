using System.Collections;
using UnityEngine;

public class TutorialUI : MonologueUI {
    public void OnCutsceneEnd(MonologueKey key) {
        LENGTH_DIVISOR = 5.0f;
        if(key != MonologueKey.L0_START) return;

        GameState.instructionQueue.Enqueue(MonologueKey.I_MOVE);
        GameState.instructionQueue.Enqueue(MonologueKey.I_INTERACT);
        GameState.instructionQueue.Enqueue(MonologueKey.I_HIGHLIGHT);
        StartCoroutine(ShowQueuedInstructions());
    }

    protected override void SetAlpha(float value) {}

    private IEnumerator ShowQueuedInstructions() {
        while(true) {
            MonologueKey key;
            var hasInstruction = GameState.instructionQueue.TryDequeue(out key);
            if(!hasInstruction) {
                yield return new WaitForSecondsRealtime(1.0f);
                continue;
            }
            StartMonologue(key);
            yield return new WaitForSecondsRealtime(10.0f);
        }
    }

    private void OnDisable() {
        StopCoroutine(ShowQueuedInstructions());
    }
}
