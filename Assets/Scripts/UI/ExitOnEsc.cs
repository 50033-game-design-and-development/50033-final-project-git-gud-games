using UnityEngine;

public class ExitOnEscape : MonoBehaviour {
    private PlayerAction _playerAction;

    private void OnEnable() {
        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.gameplay.Escape.performed += _ => {
            var fade = transform.parent.GetComponent<FadeBehaviour>();
            var nextScene = GetComponent<NextScene>();
            fade.FadeOut();
            nextScene.AllowSceneChange();
            nextScene.OnInteraction();
        };
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
}
