
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
public class CutsceneInteractable : MonoBehaviour, IInteractable {
    protected int state = 0;
    
    [SerializeField]
    protected int playState = 1;

    public virtual void OnInteraction() {
        if (state == playState) {
            PlayCutscene();    
            IncrementState();
        }
              
    }

    protected void PlayCutscene() {
        // TODO: Lock player input
        
        PlayableDirector director = GetComponent<PlayableDirector>();
        director.Play();
        
        // while (director.state == PlayState.Playing);
        // TODO: Unlock player input

    }

    public void IncrementState() {
        state++;
    }


}
