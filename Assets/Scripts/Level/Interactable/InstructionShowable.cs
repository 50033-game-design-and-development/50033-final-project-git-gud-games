using UnityEngine;

public class InstructionShowable : MonoBehaviour, IInteractable {
    public MonologueKey instruction;

    [SerializeField] private bool canQueue = true;

    public void OnInteraction() {
        if (!canQueue) {
            return;
        }
        GameState.instructionQueue.Enqueue(instruction);
        canQueue = false;
        Event.Global.showInstruction.Raise();
    }

    public void SetQueuable() {
        canQueue = true;
    }
}
