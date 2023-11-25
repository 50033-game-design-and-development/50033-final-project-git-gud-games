using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LightFlicker : MonoBehaviour
{
    // range of time delay for lights turning off
    public float minOffRange = 0.01f;
    public float maxOffRange = 1f;
    
    // range of time delay for lights turning on
    public float minOnRange = 0.01f;
    public float maxOnRange = 1f;
    
    public Material offMaterial;
    public Material onMaterial;
    
    private bool _isFlickering = false;
    private float _timeDelay;
    
    
    public bool GetFlickering()
    {
        return _isFlickering;
    }
    
    private void Update()
    {
        if (!_isFlickering)
        {
            StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight()
    {
        _isFlickering = true;
        if (offMaterial != null)
        {
            this.gameObject.transform.parent.GetComponent<Renderer>().material = offMaterial;
        }
        this.gameObject.GetComponent<Light>().enabled = false;
        _timeDelay = Random.Range(minOffRange, maxOffRange);
        yield return new WaitForSeconds(_timeDelay);
        
        if (onMaterial != null)
        {
            this.gameObject.transform.parent.GetComponent<Renderer>().material = onMaterial;
        }
        this.gameObject.GetComponent<Light>().enabled = true;
        _timeDelay = Random.Range(minOnRange, maxOnRange);
        yield return new WaitForSeconds(_timeDelay);
        _isFlickering = false;
    }

    
}
