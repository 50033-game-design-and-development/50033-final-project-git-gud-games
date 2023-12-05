using System.Collections;
using UnityEngine;

public class WindowSilhouette : MonoBehaviour {
    private Animator anim;
    [SerializeField] private float duration;
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
            yield return new WaitForSeconds(duration);
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
