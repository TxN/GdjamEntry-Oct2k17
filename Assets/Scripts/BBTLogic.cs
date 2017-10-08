using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class BBTLogic : MonoBehaviour {

	List<string> CollectedCapusles;

	void Awake() {
		EventManager.Subscribe<Event_CapsuleCollect>(this, OnCapsuleCollect);
	}
	void OnDestroy() {
		EventManager.Unsubscribe<Event_CapsuleCollect>(OnCapsuleCollect);
	}
	// Use this for initialization
	void Start () {
		CollectedCapusles = SpaceGameState.Instance.CollectedCapusles;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCapsuleCollect(Event_CapsuleCollect e) {
		//SpawnedCapsules.Remove(e.CapsuleId);
		Debug.Log("CAPSULE COLLECTED");
		string text;
		switch (e.CapsuleId) {
		case "Buisnessman":
			text = "Buisnessman";

			break;
		case "DoctorMan":
			if (!CollectedCapusles.Contains ("TeacherWoman")) {text = "DoctorMan <-> TeacherWoman quest text";}
			else text="DoctorMan";
			break;
		case "DoctorWoman":
			text = "DoctorWoman";
			break;
		case "Engineer":
			text = "Engineer";
			break;
		case "Farmer":
			text = "Farmer";
			break;
		case "Poet":
			text = "Poet";
			break;
		case "Preacher":
			if (!CollectedCapusles.Contains ("Soldier")) {text = "Preacher <-> Soldier quest text";}
			else text="Preacher";
			break;
		case "Scientist":
			if (!CollectedCapusles.Contains ("DoctorWoman")) {text = "DoctorWoman quest text";}
			else text="Scientist";
			break;
		case "Soldier":
			if (!CollectedCapusles.Contains ("Preacher")) {text = "Soldier <-> Preacher quest text";}
			else text="Soldier";
			break;
		case "Teacher":
			text = "Teacher";
			break;
		case "TeacherWoman":
			text = "TeacherWoman";
			break;
		default:
			text = "Some random dude";
			break;
		}
		SpaceGameState.Instance.BBTManager.ShowBBT(text, 5);
		//SpaceGameState.Instance.BBTManager.ShowBBT(e.CapsuleId, 5);
	}
}
