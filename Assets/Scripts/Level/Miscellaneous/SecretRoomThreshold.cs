using UnityEngine;

public class SecretRoomThreshold : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.transform.position.x < transform.position.x) {
            Event.L2.enterLivingRoom.Raise();
            Event.L2.secretRoomRevealable.Raise(false);
            Event.L2.livingRoomRevealable.Raise(true);
        } else {
            Event.L2.enterSecretRoom.Raise();
            Event.L2.secretRoomRevealable.Raise(true);
            Event.L2.livingRoomRevealable.Raise(false);
        }
    }
}
