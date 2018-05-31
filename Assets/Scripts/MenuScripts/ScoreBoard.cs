using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour {

<<<<<<< HEAD
	public Text myText;
=======
	public TextMesh myText;
>>>>>>> master
	private TopScore topScore;

	// Use this for initialization
	void Start () {
		topScore = new TopScore();
		myText.text = topScore.ToString();

        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        GetComponent<ResetGame>().Reset();
        //Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("MainMenu");
    }
}
