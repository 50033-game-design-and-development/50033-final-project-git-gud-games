using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerSilhouette : MonoBehaviour {
    // range of time delay for lights turning off
    public float minOffRange = 0.01f;
    public float maxOffRange = 1f;

    // range of time delay for lights turning on
    public float minOnRange = 0.01f;
    public float maxOnRange = 1f;
    
    // Materials for on/off, if applicable
    public Material offMaterial;
    public Material onMaterial;
    
    // list of silhouettes to appear after turning off
    public List<GameObject> silhouetteList;
    public float jumpscareProbability = 0.02f;

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
            
            // Calculate if random silhouette appears or not
            if (CalculateProbability(jumpscareProbability)) {
                int randomIndex = Random.Range(0, silhouetteList.Count);
                silhouetteList[randomIndex].SetActive(true);
                yield return new WaitForSeconds(_timeDelay);
                silhouetteList[randomIndex].SetActive(false);
            }
            else {
                yield return new WaitForSeconds(_timeDelay);
            }
            

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

    private bool CalculateProbability(float probability) {
        return Random.value < probability;
    }

    private void Start() {
        _renderer = gameObject.transform.parent.GetComponent<Renderer>();
        _light = gameObject.GetComponent<Light>();
        StartCoroutine(FlickerLight());
    }
}