using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    public FadeScreen _fader = null;
    public CanvasGroup DescriptionText = null;
    public CanvasGroup Instructions = null;

    Sequence _seq = null;
	
	void Start () {
        _seq = TweenHelper.ReplaceSequence(_seq);
        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() => { _fader.FadeWhite(1.5f); });
        _seq.AppendInterval(2.5f);
        _seq.Append(DescriptionText.DOFade(1,0.75f));
        _seq.Append(Instructions.DOFade(1,0.5f));
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonUp(0)) {
            _seq = TweenHelper.ReplaceSequence(_seq);
            _seq.AppendCallback(() => { _fader.FadeBlack(0.5f); });
            _seq.AppendInterval(0.75f);
            _seq.AppendCallback(() => { GoToScene(); });
        }


    }


    void GoToScene() {
        SceneManager.LoadScene("1");
    }
}
