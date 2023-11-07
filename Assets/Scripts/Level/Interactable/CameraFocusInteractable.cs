using System.Linq;
using UnityEngine;

public class CameraFocusInteractable : MonoBehaviour, IInteractable {
    public void OnInteraction() {
        Debug.Log("Camera focusing onto " + gameObject.name);
        // throw new System.NotImplementedException();
    }
}