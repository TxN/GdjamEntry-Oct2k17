using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using EventSys;

public class Consequences : MonoBehaviour {
    public CanvasGroup Fader              = null;
    public CanvasGroup   ImageFader       = null;
    public Image         Imageimg         = null;
	public Transform     TextBlockHolder  = null;
	public GameObject    TitleBlockFab    = null;
	public GameObject    DeadCapsules     = null;
	public CanvasGroup   DeadCapsulesCg   = null;
	public Text          DeadCapsulesText = null;
	public CanvasGroup   FinalBlock       = null;

	public Sprite citySpr  = null;
    public Sprite cataSpr  = null;
    public Sprite greenSpr = null;

    Sequence       _seq   = null;
    SpaceGameState _state = null;

	List<EndSceneTextBlock> _endsceneTexts = new List<EndSceneTextBlock>();
	int _curblock = 0;

	bool _deadCapsulesMode = false;
	bool _finalScreenMode  = false;

	void Start () {
		EventManager.Subscribe<Event_TitleBlockHide>(this, OnTextBlockHide);

		DeadCapsulesCg.gameObject.SetActive(false);

		_state = SpaceGameState.Instance;
        
        _seq = TweenHelper.ReplaceSequence(_seq);

		CreateAftermath();

        Fader.gameObject.SetActive(true);
		Fader.alpha = 1;
		Imageimg.gameObject.SetActive(true);

        _seq.Append(Fader.DOFade(0, 1));
        _seq.AppendInterval(1f);

		_seq.AppendCallback(() => { _endsceneTexts[0].Show(true); });

	}

	private void OnDestroy() {
		EventManager.Unsubscribe<Event_TitleBlockHide>(OnTextBlockHide);
	}

	void OnTextBlockHide(Event_TitleBlockHide e) {
		_curblock++;
		if ( _endsceneTexts.Count <= _curblock ) {
			HideImage();
		}  else {
			_endsceneTexts[_curblock].Show(true);
		}
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

		if ( _deadCapsulesMode && Input.GetMouseButtonUp(0) ) {
			_deadCapsulesMode = false;

			_seq = TweenHelper.ReplaceSequence(_seq, false);
			_seq.Append(DeadCapsulesCg.DOFade(0, 0.5f));
			_seq.AppendCallback( ShownEndTitles );
		}

		if ( Input.GetKeyDown(KeyCode.R)) {
			Persistence.Instance.DestroyAllPersistent(true, true);
			UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
		}
	}

	void CreateAftermath() {
		Dictionary<string, string> finalTexts = new Dictionary<string, string> () {
			{"0 0 0 0", "After a long journey to the new planet settlers had finally reached the ground of their new home. They made a treaty that could bear their existence as happy and stable society. Each of them could stand in front of a crowd and made a speech. So they decided that they should restore all traditions. Under the sky of a new planet should rise modern architecture and ancient sculptural masterpiece."},
			{"1 0 0 0", "After the landing on the planet Earth-2 great leader of colonization, comrade Roderick got into special position of power to lead and protect Humanity within its new home. Since then, he and his successors  were named “Big Comrade”, and Roderick was greatest of them. He established fair distribution of goods  and rights, where more rights goes with more responsibility!  “No clericalism for brave men in arms, he said, no primates of individuality above social needs!”"},
			{"1 1 0 0", "After the long journey to the planet named Paradise, the captain of the Ark, Roderick the First, led faithful society. Righteous men of God, headed by father Martin, fought and sacrificed heretics, who were worshipping horrible satanic “science”, since that struggle everyone started to live in peace and love."},
			{"1 1 0 1", "After a long terrifying journey to the planet named Paradise, captain of Ark, Roderick the First, lead a society with a strong hand and righteous beliefs. As the society grew, new drawback got across its way. Honored men of science under the fair arm of Roderick proceeded a lot of researches and overpassed every wicked call from vicious nature."},
			{"1 0 0 1", "After the landing on the planet Earth-2 great leader of colonization, comrade Roderick got into special position of power to lead and protect Humanity within its new home. Since then, he and his successors  were named “Big Comrade”, and Roderick was greatest of them. He established fair distribution of goods  and rights, where more rights goes with more responsibility!  “No clericalism for brave men in arms, he said, no primates of individuality above social needs!”"},
			{"0 1 0 0", "After the tragic situation on the way to the planet you discovered that preacher Martin lost his son in the crash. Martin preached everyone to be kind to each other and remember your siblings with warmth in your heart. So, after the landing , your civilization grew out of religious commune, where your collective worship praise god and family."},
			{"0 1 1 0", "After tragic situation on the way to the planet you discovered that preacher Martin lost his son in the crash. Martin preached everyone to be kind to each other and remember your siblings with warmth in your heart. So, after the landing , your civilization grew out of religious commune, where you worship the god through depicting of your family in art."},
			{"0 1 1 1", "The Exodus has come to an end. After long and exhausting trip through the cosmic desert the Ship landed on the planet named “Solomon Valley”. Fathers-founders walked on its dusty ground and shook their hands for a great future, based on rational approach to the development and life. As a sprout entwined City of Fathers Solomon Valley. Old Military Roderick established law and order, Father Martin taught people how to praise the God, and Professor Sych stated the principles of investigation."},
			{"0 1 0 1", "The Exodus has come to an end. After long and exhausting trip through the cosmic desert the Ship landed on the planet named “Solomon Valley”. Fathers-founders walked on its dusty ground and shook their hands for a great future, based on rational approach to the development and life. As a sprout entwined City of Fathers Solomon Valley. Old Military Roderick established law and order, Father Martin taught people how to praise the God, and Professor Sych stated the principles of investigation."},
			{"0 0 1 0", "Albert, a strange businessman from earth, was picked up on your ship right after the crash. There was no day without a boiling activity of people and their close interactions ever since. After the landing of the ship Albert led his friends to prosperity, but not as a strong leader. He put an idea to every head that our life belongs only to ourselves."},
			{"0 0 0 1", "After a long journey to the new planet settlers had finally reached the ground of their new home. They built a commune and took a role for each of them as his role could be bear by the person. In the beginning, they made a Bill of Andrej Right – principal law of rational life approach. Next, they built a commune and took a role for each of them as his role could be bear by the person."},
		};

        Dictionary<string, string> questWinTexts = new Dictionary<string, string>() {
            {"0 0 0 0", " So the culture was above their heads, and people tried to overpass it, jump higher than clouds. They stretched the globe above and reached the mix of stories and situations. “The real life”, as you can call it."},
            {"1 0 0 0", "As the society grew, new drawback got across its way. Honored men of science under the fair arm of comrade proceeded a lot of researches and overpassed every wicked call from nature. Hail to the society and Big Comrade!"},
            {"1 1 0 1", "Hence, every obstacle before the new civilization fell as a weak fence. As it’s said, God accompanies faithful. And so was built the Heaven on earth."},
            {"1 1 0 0", ""},// netu
            {"1 0 0 1", "As the society grew, new drawback got across its way. Honored men of science under the fair arm of comrade proceeded a lot of researches and overpassed every wicked call from nature. Hail to the society and Big Comrade!"},
            {"0 1 1 0", ""},
            {"0 1 0 0", "And this culture grows with blossom even now."},
            {"0 1 1 1", "Hence, every obstacle before the new civilization fell as a weak fence. As it’s said, God accompanies faithful. And so was built the Heaven on earth."},
            {"0 1 0 1", "Hence, every obstacle before the new civilization fell as a weak fence. As it’s said, God accompanies faithful. And so was built the Heaven on earth."},
            {"0 0 1 0", "And it was great."},
            {"0 0 0 1", ""},// netu


        };

        Dictionary<string, string> questFailTexts = new Dictionary<string, string>() {
            {"0 0 0 0", "Despite every single moment of their freedom, it could not save them from the reality, where bad thing happened. People of this civilization were trying to escape from pain, but it couldn’t work. As the poet said, carpe diem, future is already here."},
            {"1 0 0 0", "Despite titanic efforts of comrade, the lack of knowledge in chemistry and medicine played its wicked role. After 56 years of colonization process came across with terrible virus and epidemic disease killed everyone. No one could predict that."},
            {"1 1 0 0", "Despite titanic efforts of comrade the lack of knowledge in chemistry and medicine played its wicked role. After 56 years of colonization process came across with terrible virus and epidemic disease killed everyone. No one could predict that."},
            {"1 1 0 1", "But happiness was not so long. The planet was tortured by technological abuse. After a 300 years of prosperity humanity left the planet as its last breath sounded."},
            {"1 0 0 1", "Despite titanic efforts of comrade, the lack of knowledge in chemistry and medicine played its wicked role. After 56 years of colonization process came across with terrible virus and epidemic disease killed everyone. No one could predict that."},
            {"0 1 1 0", "Despite titanic efforts of Father Martin, the lack of knowledge in medicine played its wicked role. After 56 years of colonization they came across the terrible virus and epidemic disease killed everyone. No one could predict that. I, the last survivor, who will also slumber into the death soon, has nothing left but bitter truth: it’s only God who roll the Dice."},
            {"0 1 1 1", "But happiness was not so long. The planet was tortured by technological abuse. After a 300 years of prosperity humanity left the planet as its last breath sounded."},
            {"0 1 0 1", "But happiness was not so long. The planet was tortured by technological abuse. After a 300 years of prosperity humanity left the planet as its last breath sounded."},
            {"0 0 1 0", "To our shame, that society has divided too much. Prosperity of every single person without a thought about common wealth brings strife and disorder. In the end, the society fell into civil war. Lasers were delineating the sky, corpses lying around. And everything become authoritarian."},
        };

        string defaultText = "After a long journey to the new planet settlers had finally reached the ground of their new home. They made a treaty that could bear their existence as happy and stable society. Each of them could stand in front of a crowd and made a speech. So they decided that they should restore all traditions. Under the sky of a new planet should rise modern architecture and ancient sculptural masterpiece.";

		int[] final_state = { 0, 0, 0, 0 };
		if (_state.IsOnBoard("Soldier")) {final_state[0] = 1;}
		if (_state.IsOnBoard("Preacher") && _state.IsOnBoard("Poet")) {final_state[1] = 1;}
		if (_state.IsOnBoard("Buisnessman")&& ( final_state[0] == 0) ) {final_state[2] = 1;}
		if (_state.IsOnBoard("Engineer") && (final_state[2] == 0)) {final_state[3] = 1;}
		if ((_state.IsOnBoard("TeacherWoman") || _state.IsOnBoard("Teacher")) && _state.IsOnBoard("Farmer")) {final_state[3] = 0;}

        string k = final_state[0] + " " + final_state[1] + " " + final_state[2] + " " + final_state[3];

		int blockIndex = 0;

		string mainBlockText   = defaultText;
		bool   isDefaultEnding = !finalTexts.TryGetValue(k, out mainBlockText);
		bool   isBadEnding = true;

		bool totallyBadEnding = IsTotallyBadEnding();
		if ( totallyBadEnding ) {
			mainBlockText = "You landed to the planet, but its simplest form of life, viruses, defeated you. It’s such a tragedy, that no one could cure people.";
		}

		GameObject mainHolder = Instantiate <GameObject> (TitleBlockFab);
		mainHolder.transform.SetParent(TextBlockHolder, false);
		mainHolder.transform.SetAsLastSibling();
		EndSceneTextBlock mainBlock = mainHolder.GetComponent<EndSceneTextBlock>();
		mainBlock.Setup(mainBlockText, blockIndex);
		blockIndex++;
		_endsceneTexts.Add(mainBlock);

		if ( !totallyBadEnding ) {
			string questText = string.Empty;
				
			if (HasCompletedQuest() && !isDefaultEnding) {
				questWinTexts.TryGetValue(k, out questText);
				isBadEnding = false;
			} else if ( !isDefaultEnding ) {
				questFailTexts.TryGetValue(k, out questText);
				isBadEnding = true;
			}
			if ( !string.IsNullOrEmpty(questText)) {
				GameObject secondHolder = Instantiate<GameObject>(TitleBlockFab);
				secondHolder.transform.SetParent(TextBlockHolder, false);
				secondHolder.transform.SetAsLastSibling();
				EndSceneTextBlock secondBlock = secondHolder.GetComponent<EndSceneTextBlock>();
				secondBlock.Setup(questText, blockIndex);
				blockIndex++;
				_endsceneTexts.Add(secondBlock);
			}
		}

		SetupImage(final_state, totallyBadEnding, isBadEnding);
	}

	void HideImage() {
		_seq = TweenHelper.ReplaceSequence(_seq);
		_seq.Append(ImageFader.DOFade(0, 1));
		_seq.AppendCallback(() => { ImageFader.gameObject.SetActive(false); ShowCapsules(); });
	}

	void ShowCapsules() {
		_deadCapsulesMode = true;
		DeadCapsules.SetActive(true);
		DeadCapsulesText.text = MakePeopleList();
		_seq = TweenHelper.ReplaceSequence(_seq, false);
		DeadCapsulesCg.gameObject.SetActive(true);
		DeadCapsulesCg.alpha = 0f;
		_seq.Append(DeadCapsulesCg.DOFade(1, 1));
		float initPos =  DeadCapsulesText.transform.position.y;
		_seq.Insert(0.5f, DeadCapsulesText.transform.DOMoveY(initPos + 2300, 50f));
	}

	void ShownEndTitles() {
		_finalScreenMode = true;
		_seq = TweenHelper.ReplaceSequence(_seq, false);
		FinalBlock.gameObject.SetActive(true);
		FinalBlock.alpha = 0;
		_seq.Append(FinalBlock.DOFade(1, 1));
	}
	
	public void RestartClick() {
		Persistence.Instance.DestroyAllPersistent(true, true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
	}

	public void QuitClick() {
		Application.Quit();
	}

	string MakePeopleList()
	{
		string output = "";
		if (_state.CollectedCapusles.Count > 0) {
			output += "Saved:\n";
			for (int i = 0; i < _state.CollectedCapusles.Count; i++) {
				string pname = _state.CapsManager.GetInfo(_state.CollectedCapusles[i]).FullName;
				output += "\n" + pname;
			}
		}
		output += "\n\nFrozen forever in deep space:\n";
		for (int i = 0; i < _state.CapsManager._passengerInfoList.Count; i++) {
			bool saved = _state.IsOnBoard(_state.CapsManager._passengerInfoList[i].Id);
			if (!saved) {
				string pname = _state.CapsManager._passengerInfoList[i].FullName;
				if ( pname != "Unknown") {
					output += "\n" + pname;
				}
			}
		}

		return output;
	}

	bool IsTotallyBadEnding() {
		return !(_state.IsOnBoard("DoctorMan") || _state.IsOnBoard("DoctorWoman"));
	}

	void SetupImage(int[] final_state, bool isTotallyBad, bool isBad) {
		if ( isTotallyBad ) {
			Imageimg.sprite = cataSpr;
			return;
		}

		if ( isBad ) {
			Imageimg.sprite = cataSpr;
		} else {
			Imageimg.sprite = final_state[3] == 1 ? citySpr : greenSpr;
		}
	}
}
