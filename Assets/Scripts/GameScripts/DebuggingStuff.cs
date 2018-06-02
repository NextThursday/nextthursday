using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingStuff : MonoBehaviour {

    public bool debug;

    public SaveHandler save;
    public Modifiers mod;

	void Update () {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                save.NextScene();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                PlayerPrefs.DeleteKey("Accessory");
                GameObject.Find("Player(Clone)").GetComponent<PlayerScript>().RefreshAccessory();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
