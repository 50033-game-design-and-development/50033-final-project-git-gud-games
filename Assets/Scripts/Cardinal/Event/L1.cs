using UnityEngine;

namespace Event {
    public class L1 : MonoBehaviour {
        public static GameEvent placeIngredient;
        public static InventoryItemGameEvent removeIngredient;
        public static GameEvent solveP2;

        [SerializeField] private GameEvent _placeIngredient;
        [SerializeField] private InventoryItemGameEvent _removeIngredient;
        [SerializeField] private GameEvent _solveP2;

        private void Start() {
            placeIngredient = _placeIngredient;
            removeIngredient = _removeIngredient;
            solveP2 = _solveP2;
        }
    }
}
