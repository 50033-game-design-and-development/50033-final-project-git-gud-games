using System.Collections;
using UnityEngine;

public class WindowSilhouette : MonoBehaviour {
    private Animator anim;
    private bool active;
    public bool Active {
        get => active;
        set => active = value;
    }

    public void OnHide() {
        if (active) {
            Destroy(gameObject);
        }
    }

    private IEnumerator Hide() {
        if (active) {
            yield return new WaitForSeconds(1);
            anim.SetTrigger("Dispel");
        }
    }

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnBecameVisible() {
        StartCoroutine("Hide");
    }
}
