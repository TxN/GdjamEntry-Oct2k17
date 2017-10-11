using UnityEngine;
using DG.Tweening;


public class MoveZSequence : MonoBehaviour {

	Sequence _seq = null;

	public float EndZ = -3;
	public float Time = 60f;

	void Start () {
		_seq = TweenHelper.ReplaceSequence(_seq);
		_seq.Append(transform.DOMoveZ(EndZ, Time));
	}

	private void OnDestroy() {
		_seq = TweenHelper.ResetSequence(_seq, false);
	}
}
