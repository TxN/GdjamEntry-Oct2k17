using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Consequences : MonoBehaviour {
    public Text EndText = null;

    SpaceGameState _state = null;
	void Start () {
        EndText.text = MakeEndText();
        _state = SpaceGameState.Instance; 
	}
	
	void Update () {
		
	}

    string MakeEndText() {
        string output = "";

      /* if () {

        }

       * */
        if (_state.CollectedCapusles.Count > 0) {
            output += "\nSaved\n:";
            for (int i = 0; i < _state.CollectedCapusles.Count; i++) {
                string pname = _state.CapsManager.GetInfo(_state.CollectedCapusles[i]).Id;
                output += "\n- "+ pname;
            }
        }

        


        output += "\n2017\nArtGames Jam Novosibirsk";
        return output;
    }
}
