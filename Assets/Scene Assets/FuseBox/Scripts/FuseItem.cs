using UnityEngine;

namespace FuseboxSystem
{
    public class FuseItem : MonoBehaviour
    {
        [SerializeField] private ObjectType _objectType = ObjectType.None;
        private enum ObjectType { None, Fusebox, Fuse }

        [Header("Sound Effect Scriptables (Only for Fuse)")]
        [SerializeField] private Sound pickupSound = null;

        private FBController fuseboxController;

        void Awake()
        {
            switch (_objectType) 
            {
                case ObjectType.Fusebox:
                    fuseboxController = GetComponent<FBController>();
                    break;
            }
        }

        public void ObjectInteract()
        {
            switch (_objectType)
            {
                case ObjectType.Fusebox:
                    fuseboxController.CheckFuseBox();
                    break;
                case ObjectType.Fuse:
                    FBInventoryManager.instance.AddFuse();
                    FBAudioManager.instance.Play(pickupSound);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
