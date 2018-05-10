using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoader : MonoBehaviour {

    public string levelToLoad;

	void Start () {
        PlayerPrefs.DeleteKey("SkipTutorial");
        GetComponent<ResetGame>().Reset();
        Application.LoadLevel(levelToLoad);
	}
}
