using UnityEngine;

namespace Event {
    public class L1 : MonoBehaviour {
        public static GameEvent placeIngredient;
        public static InventoryItemGameEvent removeIngredient;
        public static GameEvent solveP2;
        public static GameEvent seeBook;
        public static GameEvent solveP1;
        public static GameEvent drinkStew;
        public static GameEvent unlockDoor;

        [SerializeField] private  GameEvent _drinkStew;
        [SerializeField] private  GameEvent _L1unlockDoor;
        [SerializeField] private GameEvent _placeIngredient;
        [SerializeField] private InventoryItemGameEvent _removeIngredient;
        [SerializeField] private  GameEvent _seeBook;
        [SerializeField] private  GameEvent _solveP1;
        [SerializeField] private GameEvent _solveP2;
        

        private void Start() {
            placeIngredient = _placeIngredient;
            removeIngredient = _removeIngredient;
            solveP2 = _solveP2;
            seeBook = _seeBook;
            solveP1 = _solveP1;
            solveP2 = _solveP2;
            drinkStew = _drinkStew;
            unlockDoor = _L1unlockDoor;
        }
    }
}
