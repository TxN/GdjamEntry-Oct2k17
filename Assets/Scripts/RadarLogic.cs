using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarLogic : MonoBehaviour {
	public GameObject dot;
	public Dictionary<string, GameObject> dots;
	public float vecCoef = 0.1f;
	Dictionary<string, GameObject> capsules = SpaceGameState.Instance.SpawnedCapsules;
	// Use this for initialization
	void Start () {
		
		//dots = new GameObject[capsules.Count];
		foreach (var item in capsules) {
			GameObject d = Instantiate (dot);
			dots.Add(item.Key, d);
		}
	}

	
	// Update is called once per frame
	void Update () {
		foreach (var item in dots) {
			item.Value.transform.position = GetDotPosition (item.Key);
		}
	}

	Vector3 GetDotPosition(string key){
		GameObject c = null;
		Vector3 capsulePos = new Vector3(0,0,0);
		if (capsules.TryGetValue (key, out c)) {
			capsulePos = c.transform.position;
			Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
			Vector3 relativePos = playerPos - capsulePos;
			relativePos *= vecCoef;
			return relativePos;
		} else
			return capsulePos;

		//Vector3.ClampMagnitude(relativePos, 
	}
}
