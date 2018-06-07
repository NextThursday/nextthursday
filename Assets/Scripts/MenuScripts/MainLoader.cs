using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoader : MonoBehaviour {

    public string levelToLoad;
    public float delayTime;

	void Start () {
<<<<<<< HEAD
        PlayerPrefs.DeleteKey("SkipTutorial");
=======
      //  PlayerPrefs.DeleteKey("SkipTutorial");
        GetComponent<ResetGame>().Reset();
>>>>>>> e83ce7233c65f464e6065a3984c2cf8994bea46c
        StartCoroutine(NextLevel(delayTime));

	}

    IEnumerator NextLevel (float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(levelToLoad);

    }
}
