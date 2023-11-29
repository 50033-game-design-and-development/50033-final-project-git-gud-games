using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualCutsceneController : MonoBehaviour
{
    [SerializeField] private GameObject pentagram;
    [SerializeField] private ParticleSystem circleFX;
    [SerializeField] private AudioClip extinguishSound;
    [SerializeField] private AudioClip igniteSound;
    [SerializeField] private LockedCameraFocusable cauldronCameraFocusable;
    [SerializeField] private LockedCameraFocusable fpCameraFocusable;
    [SerializeField] private GameObject[] candleFlames;


    private AudioSource audioSource;

    public void EnableGlowPentagram()
    {
        Material mat = pentagram.GetComponent<Renderer>().material;
        StartCoroutine(gradualGlow(0.5f, mat));

        EnableFlames();

    }

    public void DisableGlowPentagram()
    {
        Material mat = pentagram.GetComponent<Renderer>().material;
        StartCoroutine(gradualGlow(0.5f, mat, 1, 0));

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

    public void EnableFlames()
    {
        foreach (GameObject flame in candleFlames)
        {
            flame.SetActive(true);
        }
    }

    public void EnlargeFlames()
    {
        foreach (GameObject flame in candleFlames)
        {
            flame.GetComponent<Animator>().SetTrigger("Enlarge");
            audioSource.PlayOneShot(igniteSound);
        }
    }

    public void ShrinkFlames()
    {
        foreach (GameObject flame in candleFlames)
        {
            flame.GetComponent<Animator>().SetTrigger("Shrink");
        }
    }

    public void DisableFlames(float delay)
    {
        StartCoroutine(DisableFlameCoroutine(delay));
    }

    IEnumerator DisableFlameCoroutine(float delay)
    {
        foreach (GameObject flame in candleFlames)
        {
            flame.SetActive(false);
            audioSource.PlayOneShot(extinguishSound);
            yield return new WaitForSeconds(delay);
        }
    }

    public void EnableParticles()
    {
        circleFX.Play();
    }

    public void DisableParticles()
    {
        circleFX.Stop();

    }

    public void FocusOnCauldron()
    {
        GameState.isPuzzleLocked = false;
        cauldronCameraFocusable.OnInteraction();
    }

    public void FocusFirstPerson()
    {
        GameState.isPuzzleLocked = false;
        fpCameraFocusable.OnInteraction();
    }

    public void Escape() {
        cauldronCameraFocusable.ForcedEscape();
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DisableParticles();

    }
}
