using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSys;

public class RadarLogic : MonoBehaviour {
	public GameObject Dot     = null;
	public float      vecCoef = 100f;
	public float      maxAmpl = 100;

	Dictionary<string, GameObject> _capsules;
	Dictionary<string, GameObject> _dots;

	bool _first = true;

	void Start () {
        EventManager.Subscribe<Event_CapsuleCollect>(this, OnCapsuleCollect);
		EventManager.Subscribe<Event_Activate_Quest> (this, OnActivateQuest);
		EventManager.Subscribe<Event_CapsuleInteract>(this, OnInteractWithCapsule);
	}

    void OnDestroy() {
        EventManager.Unsubscribe<Event_CapsuleCollect>(OnCapsuleCollect);
		EventManager.Unsubscribe<Event_Activate_Quest>(OnActivateQuest);
		EventManager.Unsubscribe<Event_CapsuleInteract>(OnInteractWithCapsule);
    }
	
	void Update () {
		if (_first) {
			_first = false;
			_capsules = SpaceGameState.Instance.SpawnedCapsules;
			_dots = new Dictionary<string, GameObject>();
			foreach (var item in _capsules) {
				GameObject d = Instantiate (Dot);
				d.SetActive (true);
				d.transform.SetParent (this.transform);
				_dots.Add(item.Key, d);
			}
		}
		if (_dots.Count != 0) {
			foreach (var item in _dots) {
				item.Value.transform.localPosition = GetDotPosition (item.Key);
			}
		}
	}

	Vector3 GetDotPosition(string key){
		GameObject c = null;
		Vector3 capsulePos = new Vector3(0,0,0);
		if (_capsules.TryGetValue (key, out c)) {
			capsulePos = c.transform.position;
			Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
			Vector3 relativePos =  capsulePos - playerPos;

			relativePos *= vecCoef;
			relativePos = Vector3.ClampMagnitude(relativePos, maxAmpl);
			return relativePos;
		} else {
			return capsulePos;
		}
			
	}

    void OnCapsuleCollect(Event_CapsuleCollect e) {
        GameObject dot = null;
        if (_dots.TryGetValue(e.CapsuleId, out dot)) {
            _dots.Remove(e.CapsuleId);
            Destroy(dot);
        }
    }

	void OnActivateQuest(Event_Activate_Quest e) {
		_dots [e.Id].GetComponent<Image> ().color = Color.green;
		_dots[e.Id].transform.SetAsLastSibling();
	}

	void OnInteractWithCapsule(Event_CapsuleInteract e) {
		_dots[e.CapsuleId].GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
		_dots[e.CapsuleId].transform.SetAsFirstSibling();
	}
}
