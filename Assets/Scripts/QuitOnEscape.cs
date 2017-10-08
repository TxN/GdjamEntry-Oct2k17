using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnEscape : MonoBehaviour {
    
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}
}
