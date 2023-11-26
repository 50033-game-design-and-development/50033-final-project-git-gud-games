using UnityEngine;

namespace Event {
    public class L1 : MonoBehaviour {
        public static GameEvent drinkStew;
        public static GameEvent placeIngredient;
        public static InventoryItemGameEvent removeIngredient;
        public static GameEvent solveP1;
        public static GameEvent solveP2;
        public static GameEvent unlockDoor;

        [SerializeField] private  GameEvent _drinkStew;
        [SerializeField] private GameEvent _placeIngredient;
        [SerializeField] private InventoryItemGameEvent _removeIngredient;
        [SerializeField] private  GameEvent _solveP1;
        [SerializeField] private GameEvent _solveP2;
        [SerializeField] private  GameEvent _unlockDoor;

        private void Start() {
            placeIngredient = _placeIngredient;
            removeIngredient = _removeIngredient;
            solveP1 = _solveP1;
            solveP2 = _solveP2;
            drinkStew = _drinkStew;
            unlockDoor = _unlockDoor;
        }
    }
}
