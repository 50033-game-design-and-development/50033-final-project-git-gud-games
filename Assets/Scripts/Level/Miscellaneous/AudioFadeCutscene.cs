using UnityEngine;

public class AudioFadeCutscene : AudioFade
{
    [SerializeField] private MonologueKey _targetKey;

    public void FadeOutAfterMonologue(MonologueKey key) {
        if (key == _targetKey) {
            base.FadeOut();
        }
    }

    public void FadeInAfterMonologue(MonologueKey key) {
        if (key == _targetKey) {
            base.FadeIn();
        }
    }
}
