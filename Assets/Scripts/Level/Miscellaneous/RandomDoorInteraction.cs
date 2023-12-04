using System.Collections;
using UnityEngine;

public class RandomDoorInteraction : MonoBehaviour {
    [SerializeField] private float chance;
    private Animator anim;

    private IEnumerator RandomInteractions() {
        yield return new WaitForSeconds(77);
        if (Random.value < chance) {
            anim.SetTrigger("Interact");
        }
    }

    private void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine("RandomInteractions");
    }
}
