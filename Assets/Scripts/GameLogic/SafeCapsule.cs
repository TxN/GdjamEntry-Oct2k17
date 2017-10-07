using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCapsule : MonoBehaviour {
    public string CharacterId = "";
    public bool WasInteracted = false;
    public bool IsVisibleOnRadar = true;
    [HideInInspector]
    public float LastInteractTime = 0;
}
