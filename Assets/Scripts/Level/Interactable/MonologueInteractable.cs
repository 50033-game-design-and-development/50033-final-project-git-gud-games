using System.Linq;
using UnityEngine;

public class MonologueInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Setting monologue for " + gameObject.name);
        // throw new System.NotImplementedException();
    }
}