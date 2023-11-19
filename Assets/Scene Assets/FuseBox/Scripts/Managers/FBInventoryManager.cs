using UnityEngine;

namespace FuseboxSystem
{
    public class FBInventoryManager : MonoBehaviour
    {
        [Header("Fuses in Inventory")]
        public int inventoryFuses;

        public static FBInventoryManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void AddFuse()
        {
            inventoryFuses++;
            FBUIManager.instance.UpdateFuseUI(inventoryFuses);
        }

        public void RemoveFuse()
        {
            inventoryFuses--;
            FBUIManager.instance.UpdateFuseUI(inventoryFuses);
        }
    }
}
