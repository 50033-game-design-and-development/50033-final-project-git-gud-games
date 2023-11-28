using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualCutsceneController : MonoBehaviour
{
    [SerializeField] private GameObject pentagram;

    public void EnableGlowPentagram()
    {
        Debug.Log("START GLOWwWW");
        Material mat = pentagram.GetComponent<Renderer>().material;
        StartCoroutine(gradualGlow(1f, mat));

    }

    public void DisableGlowPentagram()
    {
        Material mat = pentagram.GetComponent<Renderer>().material;
        StartCoroutine(gradualGlow(1f, mat, 1, 0));

    }

    IEnumerator gradualGlow(float cycleTime, Material mat, float startAlpha = 0, float endAlpha = 1)
    {
        Color startColor = mat.color;
        startColor.a = startAlpha;

        Color endColor = mat.color;
        endColor.a = endAlpha;

        float currentTime = 0;
        while (currentTime < cycleTime)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / cycleTime;
            Color currentColor = Color.Lerp(startColor, endColor, t);
            mat.color = currentColor;
            pentagram.GetComponent<Renderer>().material = mat;
            yield return null;
        }
    }

    public void EnlargeFlames()
    {
        Event.L2.enlargeFlames.Raise();
    }
}
