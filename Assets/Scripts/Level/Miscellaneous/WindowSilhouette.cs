using System.Collections;
using UnityEngine;

public class WindowSilhouette : MonoBehaviour {
    private Animator _anim;
    [SerializeField] private float duration;
    private bool _active;
    private static readonly int DISPEL = Animator.StringToHash("Dispel");

    public void SetActive() {
        Debug.Log("test");
        _active = true;
    }

    public void OnHide() {
        if (_active) {
            Destroy(gameObject);
        }
    }

    private IEnumerator Hide() {
        if (!_active) {
            yield break;
        }

        Debug.Log("test");
        yield return new WaitForSeconds(duration);
        _anim.SetTrigger(DISPEL);
    }

    private void Start() {
        _anim = GetComponent<Animator>();
    }

    private void OnBecameVisible() {
        Debug.Log("started");
        StartCoroutine("Hide");
    }
}
