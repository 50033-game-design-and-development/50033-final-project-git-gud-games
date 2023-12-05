using System.Collections;
using UnityEngine;

public class TutorialUI : MonologueUI {
    public void OnCutsceneEnd(MonologueKey key) {
        LENGTH_DIVISOR = 15.0f;
        if(key != MonologueKey.L0_START) return;

        GameState.instructionQueue.Enqueue(MonologueKey.I_MOVE);
        GameState.instructionQueue.Enqueue(MonologueKey.I_INTERACT);
        GameState.instructionQueue.Enqueue(MonologueKey.I_HIGHLIGHT);
        GameState.instructionQueue.Enqueue(MonologueKey.I_SKIP);
        StartCoroutine(ShowQueuedInstructions());
    }

    protected override void SetAlpha(float value) {}

    private IEnumerator ShowQueuedInstructions() {
        bool hasShownFocus = false;
        while(true) {
            MonologueKey key;
            var hasInstruction = GameState.instructionQueue.TryDequeue(out key);
            if(!hasInstruction || (hasShownFocus && key == MonologueKey.I_FOCUS)) {
                yield return new WaitForSecondsRealtime(1.0f);
                continue;
            }

            if (key == MonologueKey.I_FOCUS) {
                hasShownFocus = true;
            }
            StartMonologue(key);
            yield return new WaitForSecondsRealtime(6.0f);
        }
    }

    private void OnDisable() {
        StopCoroutine(ShowQueuedInstructions());
    }
}
