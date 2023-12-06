using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndingCutsceneController : MonoBehaviour {

    [Header("Player")]
    [SerializeField] private CharacterController playerController;

    [Header("Cutscene direction")]
    [SerializeField] private Transform frontTransform;
    [SerializeField] private Animation ghostAnimation;

    [Header("VFX")]
    [SerializeField] private ShaderEffect_CorruptedVram corruptedVramEffect;
    [SerializeField] private PostProcessVolume vignetteVolume;
    private Vignette vignette;
    [SerializeField] private float corruptedVramMomentaryShift = 20f;
    [SerializeField] private float corruptedVramShift = 20f;
    [SerializeField] private float vignetteMaxIntensity = 0.4f;

    public void MoveToHouseFocusable(float duration) {
        Vector3 diff = frontTransform.position - playerController.transform.position;
        Vector3 direction = diff.normalized;
        float speed = diff.magnitude / duration;
        
        StartCoroutine(MoveToCoroutine(direction, speed, duration));
    }


    public void AddMomentaryShaderEffect(float duration) {
        corruptedVramEffect.enabled = true;
        StartCoroutine(AddMomentaryShaderEffectCoroutine(duration));
    }

    public void AddShaderEffect(float duration) {
        corruptedVramEffect.enabled = true;
        StartCoroutine(AddShaderEffectCoroutine(duration));
    }

    public IEnumerator AddShaderEffectCoroutine(float duration) {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            vignette.intensity.value = Mathf.Lerp(0, vignetteMaxIntensity, (Time.time - startTime) / duration);
            corruptedVramEffect.shift = Mathf.Lerp(0, corruptedVramShift, (Time.time - startTime) / duration);
            yield return null;
        }
    }

    public IEnumerator AddMomentaryShaderEffectCoroutine(float duration) {
        float startTime = Time.time;
        float endTime = startTime + duration/2;
        float endTime2 = startTime + duration;

        while (Time.time < endTime) {
            vignette.intensity.value = Mathf.Lerp(0, vignetteMaxIntensity, (Time.time - startTime) / duration);
            corruptedVramEffect.shift = Mathf.Lerp(corruptedVramMomentaryShift, 0, (Time.time - startTime) / duration);
            yield return null;
        }

        while (Time.time < endTime2) {
            vignette.intensity.value = Mathf.Lerp(vignetteMaxIntensity, 0, (Time.time - startTime) / duration);
            corruptedVramEffect.shift = Mathf.Lerp(corruptedVramMomentaryShift, 0, (Time.time - startTime) / duration);
            yield return null;
        }

        vignette.intensity.value = 0;
        corruptedVramEffect.shift = 0;
        corruptedVramEffect.enabled = false;
    }

    private IEnumerator MoveToCoroutine(Vector3 direction, float speed, float duration) {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            playerController.Move(direction * speed * Time.deltaTime);
            yield return null;
        }
        playerController.Move(Vector3.zero);
    }

    public void GhostAttackAnimation() {
        ghostAnimation.clip = ghostAnimation.GetClip("Attack2");
        ghostAnimation.Play();
    }

    // Start is called before the first frame update
    private void Start() {
        GameState.permLockMouse = true;
        GameState.LockCursor();
        playerController.transform.LookAt(frontTransform.position);
        if (vignetteVolume.profile.TryGetSettings(out vignette)) {
            vignette.intensity.value = 0.0f;
        }

        corruptedVramEffect.shift = 0;
        corruptedVramEffect.enabled = false;

        // ghostAnimation.clip = ghostAnimation.GetClip("Attack2")
    }
}
