using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSys;

public class RadarLogic : MonoBehaviour {
	public GameObject Dot;
	public Dictionary<string, GameObject> dots;
	public float vecCoef = 100f;
	public float maxAmpl = 100;
	//Dictionary<string, GameObject> capsules = SpaceGameState.Instance.SpawnedCapsules;
	Dictionary<string, GameObject> capsules;

	private bool first = true;
	// Use this for initialization
	void Start () {
        EventManager.Subscribe<Event_CapsuleCollect>(this, OnCapsuleCollect);
		EventManager.Subscribe<Activate_Quest> (this, OnActivateQuest);
	}

    void OnDestroy() {
        EventManager.Unsubscribe<Event_CapsuleCollect>(OnCapsuleCollect);
		EventManager.Unsubscribe<Activate_Quest>(OnActivateQuest);
    }
	
	void Update () {
		if (first) {
			first = false;
			capsules = SpaceGameState.Instance.SpawnedCapsules;
			dots = new Dictionary<string, GameObject>();
			foreach (var item in capsules) {
				GameObject d = Instantiate (Dot);
				d.SetActive (true);
				d.transform.SetParent (this.transform);
				dots.Add(item.Key, d);
			}
		}
		if (dots.Count != 0) {
			foreach (var item in dots) {
				item.Value.transform.localPosition = GetDotPosition (item.Key);
			}
		}

	}

	Vector3 GetDotPosition(string key){
		GameObject c = null;
		Vector3 capsulePos = new Vector3(0,0,0);
		//Vector3 interfaceShift = new Vector3 (590, 200, 0);
		if (capsules.TryGetValue (key, out c)) {
			capsulePos = c.transform.position;
			Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
			//Vector3 relativePos = playerPos - capsulePos;
			Vector3 relativePos =  capsulePos - playerPos;

			relativePos *= vecCoef;
			relativePos = Vector3.ClampMagnitude(relativePos, maxAmpl);
			//relativePos = new Vector3 (relativePos.x + interfaceShift.x, relativePos.y + interfaceShift.y, 0);
			return relativePos;
		} else
			return capsulePos;
	}

    void OnCapsuleCollect(Event_CapsuleCollect e) {
        GameObject dot = null;
        if (dots.TryGetValue(e.CapsuleId, out dot)) {
            dots.Remove(e.CapsuleId);
            Destroy(dot);
        }
    }
	void OnActivateQuest(Activate_Quest e) {
		GameObject dot = null;
		dots [e.Id].GetComponent<Image> ().color = Color.green;
	}
}
