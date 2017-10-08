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

	void CreateAftermath(){
		Dictionary<string, string> finalTexts = new Dictionary<string, string> () {
			{ "0 0 0 0", "After a long sophisticated journey to the new planet settlers had finally reached the ground of their new home. They gathered around the spaceship and sat for a treaty that could bear their existence as happy and stable society. Each of them stood in front of the crowd and made a speech about the role of an expedition in the history of humanity. So they decided that they should restore all traditions of their predecessors: remember all rituals, read all books. Under the sky of a new planet should rise modern architecture and ancient sculptural masterpiece." },
			{"1 0 0 0", "After the landing on the planet Eart-2 great leader of colonization, comrade Roderick got into special position of power to lead and protect Humanity within its new home. Since then, he and his successors  were named “Big Comrade”, and Roderick was greatest of them. He established fair distribution of goods  and rights, where more rights goes with more responsibility!  “No clericalism for brave men in arms, he said, no primates of individuality above social needs!”"},
			{"1 1 0 0", "After the long terrifying journey to the planet named Paradise, the captain of the Ark, Roderick the First, lead the faithful society  to the heathen on earth by fire and sword of his own.  Righteous men of God fought and sacrificed heretics, who were worshipping horrible satanic “science”, since that struggle everyone started to live in peace and love."},
			{"1 1 1 0", ""}
		};

		int[] final_state = { 0, 0, 0, 0 };
		if (_state.IsOnBoard("Soldier")) {final_state[0] = 1;}
		if (_state.IsOnBoard("Preacher") & _state.IsOnBoard("Poet")) {final_state[1] = 1;}
		if (_state.IsOnBoard("Buisnessman")) {final_state[2] = 1;}
		if (_state.IsOnBoard("Engineer")) {final_state[3] = 1;}
		if ((_state.IsOnBoard("TeacherWoman") | _state.IsOnBoard("Teacher")) & _state.IsOnBoard("Farmer")) {final_state[3] = 0;}
	}
}
