using UnityEngine;

public class CameraFocusable : MonoBehaviour, IInteractable {
    public void OnInteraction() {
        Debug.Log("Camera focusing onto " + gameObject.name);
    }
}