using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] private Behaviour target;
    [SerializeField] private float duration;

    private void Start() {
        target.enabled = false;
        StartCoroutine("Countdown");
    }

    private IEnumerator Countdown() {
        yield return new WaitForSeconds(duration);
        target.enabled = true;
    }
}
