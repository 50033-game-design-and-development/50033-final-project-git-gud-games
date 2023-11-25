using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    private bool _isFlickering = false;
    private float _timeDelay;
    
    
    public bool GetFlickering() {
        return _isFlickering;
    }
    
    private void Update() {
        if (!_isFlickering) {
            StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight() {
        // Turn off
        _isFlickering = true;
        if (offMaterial != null) {
            this.gameObject.transform.parent.GetComponent<Renderer>().material = offMaterial;
        }

        if (lightAudioSource != null && offAudio != null) {
            lightAudioSource.PlayOneShot(offAudio);
        }
        this.gameObject.GetComponent<Light>().enabled = false;
        _timeDelay = Random.Range(minOffRange, maxOffRange);
        yield return new WaitForSeconds(_timeDelay);
        
        // Turn on
        if (onMaterial != null) {
            this.gameObject.transform.parent.GetComponent<Renderer>().material = onMaterial;
        }
        if (lightAudioSource != null && onAudio != null) {
            lightAudioSource.PlayOneShot(onAudio);
        }
        this.gameObject.GetComponent<Light>().enabled = true;
        _timeDelay = Random.Range(minOnRange, maxOnRange);
        yield return new WaitForSeconds(_timeDelay);
        _isFlickering = false;
    }

    
}
