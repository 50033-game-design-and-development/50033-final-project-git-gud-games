using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private PlayerAction playerAction;

    private int layerMaskInteractable;

    void OnClick(Vector2 screenPos)
    {
        Debug.Log("Clicked");
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // TODO: for the distance, use playerconstants once merged with FGD-14
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1.8f, layerMaskInteractable))
        {
            Debug.Log("Interact " + raycastHit.transform.gameObject.name);
            Interact(raycastHit.transform.gameObject);
        }
        Debug.Log("End");
    }

    void Interact(GameObject obj)
    {
        foreach (IInteractable i in obj.GetComponents<IInteractable>())
        {
            i.Interact();
        }
    }


    void Start()
    {
        layerMaskInteractable = LayerMask.GetMask("Interactable");

        playerAction = new PlayerAction();
        playerAction.Enable();
        playerAction.gameplay.MousePos.performed += ctx => OnClick(ctx.ReadValue<Vector2>());
    }


    // void Update() {

    //     // Ray points out from the middle of camera viewport 
    //     Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
    //     if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
    //         if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
    //             PerformHighlight(raycastHit.transform);
    //             return;
    //         }
    //     }

    //     highlighted = false;
    // }


}
