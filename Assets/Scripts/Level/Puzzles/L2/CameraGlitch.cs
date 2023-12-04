using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraGlitch : MonoBehaviour {

    [SerializeField] private float glitchShift = 5f;

    public void Glitch(float duration) {
        Camera camera = Camera.main;
        ShaderEffect_CorruptedVram effect = camera.AddComponent<ShaderEffect_CorruptedVram>();
        StartCoroutine(GlitchCoroutine(effect, duration));
    }

    private IEnumerator GlitchCoroutine(ShaderEffect_CorruptedVram effect, float duration) {
        effect.shift = 0;
        effect.enabled = true;
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            effect.shift = Mathf.Lerp(glitchShift, 0, (Time.time - startTime) / duration);
            yield return null;
        }
        
        Destroy(effect);
    }


}