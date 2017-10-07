using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSys;

public class SlotsGUI : MonoBehaviour {
    public List<Image> PortraitPlaces = new List<Image>();

    int _curIndex = 0;

    void Start() {
        EventManager.Subscribe<Event_CapsuleCollect>(this, OnCollect);
    }

    void OnDestroy() {
        EventManager.Unsubscribe<Event_CapsuleCollect>(OnCollect);
    }

    void OnCollect(Event_CapsuleCollect e) {
        Sprite spr = (Sprite) Resources.Load<Sprite>("SmallPortraits/" + e.CapsuleId);
        if (spr != null) {
            Debug.Log("set spr");
            PortraitPlaces[_curIndex].sprite = spr; 
        }
        _curIndex++;
    }
}
