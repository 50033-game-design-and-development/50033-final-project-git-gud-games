using UnityEngine;

public class StudyRoomThreshold : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.transform.position.z < transform.position.z) {
            Event.L2.studyRoomRevealable.Raise(false);
            Event.L2.livingRoomRevealable.Raise(true);
        } else {
            Event.L2.studyRoomRevealable.Raise(true);
            Event.L2.livingRoomRevealable.Raise(false);
        }
    }
}
