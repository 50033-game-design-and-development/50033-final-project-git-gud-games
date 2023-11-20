using UnityEngine;
using UnityEngine.UI;

namespace FuseboxSystem
{
    public class FBUIManager : MonoBehaviour
    {
        [Header("Fuse Inventory Canvas")]
        [SerializeField] private CanvasGroup fuseInventory = null;

        [Header("Fuse UI")]
        [SerializeField] private Text fuseAmountText = null;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair = null;

        private bool isOpen;

        public static FBUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        private void Update()
        {
            if (Input.GetKeyDown(FBInputManager.instance.inventoryKey))
            {
                OpenInventory();
            }
        }

        public void OpenInventory()
        {
            isOpen = !isOpen;
            fuseInventory.alpha = isOpen ? 1 : 0;
        }


        public void UpdateFuseUI(int fusesAmount)
        {
            fuseAmountText.text = fusesAmount.ToString("0");
        }

        public void CrosshairChange(bool on)
        {
            if (on)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
            }
        }
    }
}

