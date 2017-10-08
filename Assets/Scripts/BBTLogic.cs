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
		//Debug.Log("CAPSULE COLLECTED");
		string text;
		switch (e.CapsuleId) {
		case "Buisnessman":
			text = "Let's say, it was a great deal!";
			break;
		case "DoctorMan":
			if (!CollectedCapusles.Contains ("TeacherWoman")) {text = "Can you help me please? I've lost my sister, can you find her? I haven't seen her for years.";}
			else text="Ghm, 75 Beats per minute. Good enough";
			break;
		case "DoctorWoman":
			if (!CollectedCapusles.Contains ("Scientist")) {text = "Oh! Oh my! Vasiliy! Vasiliy, where are you?";}
			else text="Hello? Is anybode there?";
			break;
		case "Engineer":
			text = "Oh.. Work again? Don't worry, I will lock up the hatch.";
			break;
		case "Farmer":
			text = "Veni. Vidi. Vici.";
			break;
		case "Poet":
			text = "I will write poem about that.";
			break;
		case "Preacher":
			if (!CollectedCapusles.Contains ("Soldier")) {text = "Excuse me, sir. Where can I find my son? Oh... I see. If you'd be so kind...";}
			else text="God bless you, sir.";
			break;
		case "Scientist":
			if (!CollectedCapusles.Contains ("DoctorWoman")) {text = "Lyudmila! Lyudmila! Please, find my wife!";}
			else text="Khh-khh. Breathe, Vasiliy, breathe...";
			break;
		case "Soldier":
			if (!CollectedCapusles.Contains ("Preacher")) {text = "Sir, no preacher on the board, sir?";}
			else text="Sir, yes, SIR!";
			break;
		case "Teacher":
			text = "It was absolutly not my pillow.";
			break;
		case "TeacherWoman":
			if (!CollectedCapusles.Contains ("Scientist")) {text = "I have a brother... But I can't find him. Can... can you find him for me?";}
			else text="My head... Oh no, it hurts so much!";
			break;
		case "Noname":
			text="...Thanks.";
			break;
		default:
			text = "Some random dude";
			break;
		}
		SpaceGameState.Instance.BBTManager.ShowBBT(text, 5);
		//SpaceGameState.Instance.BBTManager.ShowBBT(e.CapsuleId, 5);
	}
}
