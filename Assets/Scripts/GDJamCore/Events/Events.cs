﻿namespace EventSys {
	public struct Event_CapsuleInteract {
		public string CapsuleId;
	}

    public struct Event_CapsuleCollect {
        public string CapsuleId;
    }

    public struct Event_Accelerate {
        public float direction;
    }

    public struct Event_Jump {

    }
    public struct Event_Activate_Quest {
        public string Id;
    }

	public struct Event_TitleBlockHide {
		public int BlockIndex;
	}

	public struct Event_TitleButtonClick {
		public string ButtonId;
	}

	public struct Event_ShipRotate {
		
	}
}
