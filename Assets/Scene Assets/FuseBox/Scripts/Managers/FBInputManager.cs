///Inputs can be found in the FBInteractor & FBUIManager

using UnityEngine;

namespace FuseboxSystem
{
    public class FBInputManager : MonoBehaviour
    {
        [Header("Raycast Interact Input")]
        public KeyCode interactKey = KeyCode.Mouse0; //Default is Mouse 0
        public KeyCode inventoryKey = KeyCode.Tab; //Default is tab

        public static FBInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
