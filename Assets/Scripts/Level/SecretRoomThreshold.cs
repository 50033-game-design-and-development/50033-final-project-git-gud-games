using UnityEngine;

public class SecretRoomThreshold : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.transform.position.x < transform.position.x) {
            Event.L2.enterLivingRoom.Raise();
        } else {
            Event.L2.enterSecretRoom.Raise();
        }
    }
}
