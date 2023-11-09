using UnityEngine;

public class PlayerInteractor : MonoBehaviour {
    private PlayerAction _playerAction;

    private int _layerMaskInteractable;

    void OnClick(Vector2 screenPos) {
        Debug.Log("Clicked");
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // TODO: for the distance, use playerconstants once merged with FGD-14
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1.8f, _layerMaskInteractable)) {
            Debug.Log("Interact " + raycastHit.transform.gameObject.name);
            Interact(raycastHit.transform.gameObject);
        }

        Debug.Log("End");
    }

    private static void Interact(GameObject obj) {
        foreach (var i in obj.GetComponents<IInteractable>()) {
            i.OnInteraction();
        }
    }


    private void Start() {
        _layerMaskInteractable = LayerMask.GetMask("Interactable");

        _playerAction = new PlayerAction();
        _playerAction.Enable();
        _playerAction.gameplay.MousePos.performed += ctx => OnClick(ctx.ReadValue<Vector2>());
    }
}
