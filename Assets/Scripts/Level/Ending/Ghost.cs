using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Material material;

    [Header("Color Fields")]
    [SerializeField] private float r;
    [SerializeField] private float g;
    [SerializeField] private float b;

    private Color currentColor;

    private void OnEnable() {
        // gradually change alpha from 0 to 1
        StartCoroutine(RaiseAlpha(0, 1, 1));

    }

    private IEnumerator RaiseAlpha(float start, float end, float duration) {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            float time = (Time.time - startTime) / duration;
            currentColor.a = Mathf.Lerp(start, end, time);
            material.SetColor("_Color", currentColor);
            yield return null;
        }
    }

    private void Start() {
        currentColor = new Color(r/255,g/255,b/255,0);
        material.SetColor("_Color", currentColor);
    }
}
