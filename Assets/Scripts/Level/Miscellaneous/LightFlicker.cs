using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
    // range of time delay for lights turning off
    public float minOffRange = 0.01f;
    public float maxOffRange = 1f;

    // range of time delay for lights turning on
    public float minOnRange = 0.01f;
    public float maxOnRange = 1f;

    public Material offMaterial;
    public Material onMaterial;

    // audio to play when on or off
    public AudioSource lightAudioSource;
    public AudioClip onAudio;
    public AudioClip offAudio;

    private Renderer _renderer;
    private Light _light;

    private float _timeDelay;

    private IEnumerator FlickerLight() {
        while(true) {
            _timeDelay = Random.Range(minOnRange, maxOnRange);
            yield return new WaitForSeconds(_timeDelay);
            // Turn off
            if (offMaterial != null) {
                _renderer.material = offMaterial;
            }

            if (lightAudioSource != null && offAudio != null) {
                lightAudioSource.PlayOneShot(offAudio);
            }

            _light.enabled = false;
            _timeDelay = Random.Range(minOffRange, maxOffRange);
            yield return new WaitForSeconds(_timeDelay);

            // Turn on
            if (onMaterial != null) {
                _renderer.material = onMaterial;
            }

            if (lightAudioSource != null && onAudio != null) {
                lightAudioSource.PlayOneShot(onAudio);
            }

            _light.enabled = true;
        }
    }

    private void Start() {
        _renderer = gameObject.transform.parent.GetComponent<Renderer>();
        _light = gameObject.GetComponent<Light>();
        StartCoroutine(FlickerLight());
    }
}
