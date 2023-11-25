using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Puzzles {
    public class StudyRoomLampsHandler : RoomLampsHandler {
        private bool _fuseInserted = false;
        
        public void OnFuseInsert() {
            _fuseInserted = true;
            OnLightsToggle(true);
        }

        public override void OnLightsToggle(bool turnOn) {
            base.OnLightsToggle(turnOn && _fuseInserted);
        }
    }
}
