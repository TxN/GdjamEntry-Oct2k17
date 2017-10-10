using UnityEngine;
using DG.Tweening;
using EventSys;

public class AnimatedButton : MonoBehaviour {

	public float FadeStartTime = 4f;

	CanvasGroup _cg;
	Sequence _seq;

	void Start () {
		Invoke("Animate", FadeStartTime);
	}

	void OnDestroy() {
		_seq = TweenHelper.ResetSequence(_seq, false);
	}

	void Animate() {
		_seq = TweenHelper.ReplaceSequence(_seq);
		_cg = GetComponent<CanvasGroup>();
		_seq.Append(_cg.DOFade(0.5f, 0.6f));
		_seq.Append(_cg.DOFade(0.9f, 0.6f));
		_seq.SetLoops(-1);
	}

}
