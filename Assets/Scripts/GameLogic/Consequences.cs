using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Consequences : MonoBehaviour {
    public Text EndText = null;
	void Start () {
        EndText.text = MakeEndText();
	}
	
	void Update () {
		
	}

    string MakeEndText() {
        string output = "";




        output += "\n2017\nArtGames Jam Novosibirsk";
        return output;
    }
}
