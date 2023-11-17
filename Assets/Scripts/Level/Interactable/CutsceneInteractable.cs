
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
public class CutsceneInteractable : MonoBehaviour, IInteractable {
    protected int state = 0;
    public virtual void OnInteraction() {
        PlayCutscene();          
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
