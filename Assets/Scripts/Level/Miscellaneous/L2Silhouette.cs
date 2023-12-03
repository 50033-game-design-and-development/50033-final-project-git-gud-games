using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class L2Silhouette : MonoBehaviour {
    [Tooltip("Probability of a silhouette appearing")]
    public float appearanceProbability = 0.3f;
    
    [Tooltip("Minimum time of visibility")]
    public float minAppearanceTime = 0.5f;
    
    [Tooltip("Maximum time of visibility")]
    public float maxAppearanceTime = 10f;

    private SpriteRenderer _silhouette;
    
    // called when lights are switched on or off. Silhouette only appears when lights are off
    public void Visible(bool state) {
        if (Random.value < appearanceProbability && !state) {
            _silhouette.enabled = true;
            StartCoroutine(VisibleTime(Random.Range(minAppearanceTime, maxAppearanceTime)));
        }
        else {
            _silhouette.enabled = false;
        }
    }
    
    // Silhouette disappears after a random time or when player enters collider or when lights are on.
    private IEnumerator VisibleTime(float time) {
        yield return new WaitForSeconds(time);
        _silhouette.enabled = false;
    }

    // When player enters its collider, silhouette disappears
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            _silhouette.enabled = false;
        }
    }

    private void Start() {
        _silhouette = this.gameObject.GetComponent<SpriteRenderer>();
    }
}
