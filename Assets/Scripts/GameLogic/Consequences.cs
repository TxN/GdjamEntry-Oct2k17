using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Consequences : MonoBehaviour {
    public Text EndText = null;
    public RectTransform Holder = null;
    public CanvasGroup Fader = null;

    Sequence _seq = null;

    SpaceGameState _state = null;
	void Start () {
        _state = SpaceGameState.Instance;
        EndText.text = MakeEndText();
        
        _seq = TweenHelper.ReplaceSequence(_seq);

        Fader.gameObject.SetActive(true);
        _seq.Append(Fader.DOFade(0, 1));
        _seq.AppendInterval(1f);
        _seq.Append(Holder.DOLocalMoveY(3000, 60));
        _seq.AppendCallback( ()=> { Application.Quit(); });

	}
	
	void Update () {
		
	}

    string MakeEndText() {
        string output = "";

        if (_state.CollectedCapusles.Count > 0) {
            output += "\nSaved:\n";
            for (int i = 0; i < _state.CollectedCapusles.Count; i++) {
                string pname = _state.CapsManager.GetInfo(_state.CollectedCapusles[i]).FullName;
                output += "\n- "+ pname;
            }
        }
        output += "\nFreezed forever in deep space:\n";
        for (int i = 0; i < _state.CapsManager._passengerInfoList.Count; i++) {
            bool saved = _state.IsOnBoard( _state.CapsManager._passengerInfoList[i].Id );
            if (!saved) {
                string pname = _state.CapsManager._passengerInfoList[i].FullName;
                output += "\n- " + pname;
            }
        }

        output += "\n\n2017\nArtGames Jam Novosibirsk";
        return output;
    }
}
