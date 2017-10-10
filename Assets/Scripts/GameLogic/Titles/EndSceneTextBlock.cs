using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventSys;

public class EndSceneTextBlock : MonoBehaviour {

	private Sequence    _seq = null;
	private CanvasGroup _cg  = null;
	private bool _inited = false;
	public Text TextBlock = null;

	private int _index = 0;

	private bool _reactToClicks = false;

	WindowState _state = WindowState.Hidden;

	public int Index {
		get {
			return _index;
		}
	}

	private void Start() {
		if (!_inited) {
			Init();
		}
	}

	private void Update() {
		if ( _reactToClicks && Input.GetMouseButtonUp(0) ) {
			Hide(true);
		}
	}

	void Init() {
		_inited = true;
		_cg = GetComponent<CanvasGroup>();
		_cg.alpha = 0f;
		gameObject.SetActive(false);
	}

	public void Setup(string text, int index) {
		if (!_inited) {
			Init();
		}
		TextBlock.text = text;
	}

	public void Show(bool clickHide = true) {
		if (_state != WindowState.Hidden) {
			return;
		}
		_state = WindowState.Shown;
		gameObject.SetActive(true);
		bool tmp = clickHide;
		_seq = TweenHelper.ReplaceSequence(_seq, false);
		_seq.Append(_cg.DOFade(1, 0.75f));
		_seq.AppendCallback( () => { _reactToClicks = tmp; });
	}

	public void Hide(bool deactivate = true) {
		if ( _state != WindowState.Shown) {
			return;
		}
		_state = WindowState.Hidden;

		_seq = TweenHelper.ReplaceSequence(_seq, false);
		_seq.Append(_cg.DOFade(0, 0.75f));
		_seq.AppendCallback( () => {
			EventManager.Fire<Event_TitleBlockHide>(new Event_TitleBlockHide { BlockIndex = _index });
			gameObject.SetActive(!deactivate);
		});
	}

	private void OnDestroy() {
		_seq = TweenHelper.ResetSequence(_seq, false);
	}
}
