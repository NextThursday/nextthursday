using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoader : MonoBehaviour {

    public string levelToLoad;
    public float delayTime;

	void Start () {
        //  PlayerPrefs.DeleteKey("SkipTutorial");
        //GetComponent<ResetGame>().Reset();
     //   PlayerPrefs.DeleteAll();
        Debug.Log("load!");
        StartCoroutine(NextLevel(delayTime));

	}

    IEnumerator NextLevel (float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("load2" + levelToLoad);
        Application.LoadLevel(levelToLoad);

    }
}
