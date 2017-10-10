using UnityEngine;
using DG.Tweening;
using EventSys;

public class ControlsHint : MonoBehaviour {

	Sequence _seq = null;
	CanvasGroup _cg = null;
	
	void Start() {
		_cg = GetComponent<CanvasGroup>();
		_cg.alpha = 1;
		EventManager.Subscribe<Event_ShipRotate>(this, OnRotate);
		EventManager.Subscribe<Event_Accelerate>(this, OnAccelerate);
		EventManager.Subscribe<Event_Jump>(this, OnJump);
	}

	void OnRotate(Event_ShipRotate e) {
		OnControlsUsed();
	}

	void OnAccelerate(Event_Accelerate e) {
		OnControlsUsed();
	}

	void OnJump(Event_Jump e) {
		OnControlsUsed();
	}

	void OnControlsUsed() {
		_seq = TweenHelper.ReplaceSequence(_seq, false);
		_seq.Append(_cg.DOFade(0, 0.75f));
		_seq.AppendCallback( () => { Destroy(gameObject); });
	}

	void OnDesroy() {
		_seq = TweenHelper.ReplaceSequence(_seq, false);
		EventManager.Unsubscribe<Event_ShipRotate>(OnRotate);
		EventManager.Unsubscribe<Event_Accelerate>(OnAccelerate);
		EventManager.Unsubscribe<Event_Jump>( OnJump);
	}
}
