using UnityEngine;
using UnityEngine.Events;

namespace FuseboxSystem
{
    public class FBController : MonoBehaviour
    {
        [Header("Fuse Inserted?")]
        [SerializeField] private bool[] fuseInserted = new bool[4];

        [Header("Individual Fuses (Parented to the fusebox)")]
        [SerializeField] private GameObject[] fuseObjects = new GameObject[4];

        [Header("Fusebox Lights (Parented to the fusebox)")]
        [SerializeField] private GameObject[] lights = new GameObject[4];

        [Header("Materials (Inside the project folder)")]
        [SerializeField] private Material greenButton = null;

        [Header("Sound Effect Scriptables")]
        [SerializeField] private Sound zapSound = null;

        [Header("Power on - When all fuses are inserted")]
        [SerializeField] private UnityEvent powerUp = null;

        private bool powerOn = false;

        void Start()
        {
            for (int i = 0; i < fuseInserted.Length; i++)
            {
                if (fuseInserted[i])
                {
                    lights[i].GetComponent<Renderer>().material = greenButton;
                    fuseObjects[i].SetActive(true);
                }
            }
        }

        void AllFusesInserted()
        {
            if (System.Array.TrueForAll(fuseInserted, inserted => inserted))
            {
                powerOn = true;
                gameObject.tag = "Untagged";
                powerUp.Invoke();
            }
        }

        public void CheckFuseBox()
        {
            if (FBInventoryManager.instance.inventoryFuses <= 0 && !powerOn)
            {
                ZapAudio();
            }

            if (FBInventoryManager.instance.inventoryFuses >= 1 && !powerOn)
            {
                for (int i = 0; i < fuseInserted.Length; i++)
                {
                    if (!fuseInserted[i])
                    {
                        fuseObjects[i].SetActive(true);
                        fuseInserted[i] = true;
                        lights[i].GetComponent<Renderer>().material = greenButton;
                        FBInventoryManager.instance.RemoveFuse();
                        ZapAudio();
                        AllFusesInserted();
                        break;
                    }
                }
            }
        }

        void ZapAudio()
        {
            FBAudioManager.instance.Play(zapSound);
        }
    }
}
