using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;
	private TopScore topScore;

    void Start () {
        string gameState = PlayerPrefs.GetString("GameEndState");
        if (gameState == "WIN")
        {
            gameComplete.text = "You have won!";
        }
        else if (gameState == "DEATH")
        {
            gameComplete.text = "You have died.";
        }

		topScore = new TopScore();
		int myScore = PlayerPrefs.GetInt("GameScore");
        score.text = "Score: " + myScore;
		int rank = topScore.GetRank(myScore);
		if (rank < 10){
			score.text += "\n" + "You are now on Top " + (rank + 1);
		}
		topScore.AddScore("Player", myScore);
        StartCoroutine(End());
	}

    IEnumerator End ()
    {
        yield return new WaitForSeconds(5);
        //GetComponent<ResetGame>().Reset();
        //Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("HighScore");
        
    }
	
}
