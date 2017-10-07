using UnityEngine;

namespace EventSys {
    public struct Event_CapsuleCollect {
        public string CapsuleId;
    }

    public struct Event_CapsuleDrop {
        public string CapsuleId;
    }

    public struct Event_Accelerate {
        public float direction;
    }
}
