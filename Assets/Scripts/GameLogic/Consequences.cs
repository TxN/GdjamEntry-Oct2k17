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

    bool HasCompletedQuest() {
        if (_state.IsOnBoard("Scientist") && _state.IsOnBoard("DoctorWoman")) {
            return true;
        }
        if (_state.IsOnBoard("DoctorMan") && _state.IsOnBoard("TeacherWoman")) {
            return true;
        }
        if (_state.IsOnBoard("Preacher") && _state.IsOnBoard("Soldier")) {
            return true;
        }
        return false;
    }	

	void Update () {
		
	}

    string MakeEndText() {
        string output = CreateAftermath();

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

	string CreateAftermath(){
		Dictionary<string, string> finalTexts = new Dictionary<string, string> () {
			{"0 0 0 0", "After a long journey to the new planet settlers had finally reached the ground of their new home. They made a treaty that could bear their existence as happy and stable society. Each of them could stand in front of a crowd and made a speech. So they decided that they should restore all traditions. Under the sky of a new planet should rise modern architecture and ancient sculptural masterpiece."},
			{"1 0 0 0", "After the landing on the planet Eart-2 great leader of colonization, comrade Roderick got into special position of power to lead and protect Humanity within its new home. Since then, he and his successors  were named “Big Comrade”, and Roderick was greatest of them. He established fair distribution of goods  and rights, where more rights goes with more responsibility!  “No clericalism for brave men in arms, he said, no primates of individuality above social needs!”"},
			{"1 1 0 0", "After the long journey to the planet named Paradise, the captain of the Ark, Roderick the First, led faithful society. Righteous men of God, headed by father Martin, fought and sacrificed heretics, who were worshipping horrible satanic “science”, since that struggle everyone started to live in peace and love."},
			{"1 1 0 1", "After a long terrifying journey to the planet named Paradise, captain of Ark, Roderick the First, lead a society with a strong hand and righteous beliefs. As the society grew, new drawback got across its way. Honored men of science under the fair arm of Roderick proceeded a lot of researches and overpassed every wicked call from vicious nature."},
			{"1 0 0 1", "After the landing on the planet Eart-2 great leader of colonization, comrade Roderick got into special position of power to lead and protect Humanity within its new home. Since then, he and his successors  were named “Big Comrade”, and Roderick was greatest of them. He established fair distribution of goods  and rights, where more rights goes with more responsibility!  “No clericalism for brave men in arms, he said, no primates of individuality above social needs!”"},
			{"0 1 0 0", "After the tragic situation on the way to the planet you discovered that preacher Martin lost his son in the crash. Martin preached everyone to be kind to each other and remember your siblings with warmth in your heart. So, after the landing , your civilization grew out of religious commune, where your collective worship praise god and family."},
			{"0 1 1 0", "After tragic situation on the way to the planet you discovered that preacher Martin lost his son in the crash. Martin preached everyone to be kind to each other and remember your siblings with warmth in your heart. So, after the landing , your civilization grew out of religious commune, where you worship the god through depicting of your family in art."},
			{"0 1 1 1", "The Exodus has come to an end. After long and exhausting trip through the cosmic desert the Ship landed on the planet named “Solomon Valley”. Fathers-founders walked on its dusty ground and shook their hands for a great future, based on rational approach to the development and life. As a sprout entwined City of Fathers Solomon Valley. Old Military Roderick established law and order, Father Martin taught people how to praise the God, and Professor Sych stated the principles of investigation."},
			{"0 1 0 1", "The Exodus has come to an end. After long and exhausting trip through the cosmic desert the Ship landed on the planet named “Solomon Valley”. Fathers-founders walked on its dusty ground and shook their hands for a great future, based on rational approach to the development and life. As a sprout entwined City of Fathers Solomon Valley. Old Military Roderick established law and order, Father Martin taught people how to praise the God, and Professor Sych stated the principles of investigation."},
			{"0 0 1 0", "Albert, a strange businessman from earth, was picked up on your ship right after the crash. There was no day without a boiling activity of people and their close interactions ever since. After the landing of the ship Albert led his friends to prosperity, but not as a strong leader. He put an idea to every head that our life belongs only to ourselves."},
			{"0 0 0 1", "After a long journey to the new planet settlers had finally reached the ground of their new home. They built a commune and took a role for each of them as his role could be bear by the person. In the beginning, they made a Bill of Andrej Right – principal law of rational life approach. Next, they built a commune and took a role for each of them as his role could be bear by the person."},
		};

        Dictionary<string, string> questWinTexts = new Dictionary<string, string>();

        Dictionary<string, string> questFailTexts = new Dictionary<string, string>();

        string defaultText = "After a long journey to the new planet settlers had finally reached the ground of their new home. They made a treaty that could bear their existence as happy and stable society. Each of them could stand in front of a crowd and made a speech. So they decided that they should restore all traditions. Under the sky of a new planet should rise modern architecture and ancient sculptural masterpiece.";

		int[] final_state = { 0, 0, 0, 0 };
		if (_state.IsOnBoard("Soldier")) {final_state[0] = 1;}
		if (_state.IsOnBoard("Preacher") && _state.IsOnBoard("Poet")) {final_state[1] = 1;}
		if (_state.IsOnBoard("Buisnessman")&& ( final_state[0] == 0) ) {final_state[2] = 1;}
		if (_state.IsOnBoard("Engineer") && (final_state[2] == 0)) {final_state[3] = 1;}
		if ((_state.IsOnBoard("TeacherWoman") || _state.IsOnBoard("Teacher")) && _state.IsOnBoard("Farmer")) {final_state[3] = 0;}

        string k = final_state[0] + " " + final_state[1] + " " + final_state[2] + " " + final_state[3];

        string res = defaultText;

        if (finalTexts.TryGetValue(k, out res)) {
            if ( HasCompletedQuest()) {
                string adding = "\n";
                questWinTexts.TryGetValue(k, out adding);
                res += adding;
            } else {
                string adding = "\n";
                questFailTexts.TryGetValue(k, out adding);
                res += adding;
            }
        } else {
            res = defaultText;
        }

        if (!( _state.IsOnBoard("DoctorMan") || _state.IsOnBoard("DoctorWoman"))) {
            res = "You landed to the planet, but its simplest form of life, viruses, defeated you. It’s such a tragedy, that no one could cure people.";
        }

        return res;
	}
}
