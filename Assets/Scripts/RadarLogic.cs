using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarLogic : MonoBehaviour {
	public GameObject dot;
	public Dictionary<string, GameObject> dots;
	public float vecCoef = 100f;
	//Dictionary<string, GameObject> capsules = SpaceGameState.Instance.SpawnedCapsules;
	Dictionary<string, GameObject> capsules;

	private bool first = true;
	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		if (first) {
			first = false;
			capsules = SpaceGameState.Instance.SpawnedCapsules;
			dots = new Dictionary<string, GameObject>();
			foreach (var item in capsules) {
				GameObject d = Instantiate (dot);
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
			//relativePos = new Vector3 (relativePos.x + interfaceShift.x, relativePos.y + interfaceShift.y, 0);
			return relativePos;
		} else
			return capsulePos;

		//Vector3.ClampMagnitude(relativePos, 
	}
}
