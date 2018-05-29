using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;

    public bool debug;
    public int debug_score;

    void Start () {
        if (debug)
        {
            PlayerPrefs.SetInt("GameScore", debug_score);
            PlayerPrefs.SetString("GameEndState", "WIN");
        }



        string gameState = PlayerPrefs.GetString("GameEndState");
		int myScore = 0;
        if (gameState == "WIN")
        {
			TopScore topScore = new TopScore();
            myScore = PlayerPrefs.GetInt("GameScore");
            int rank = topScore.GetRank(myScore);
            if (rank < 10)
            {
				gameComplete.text = "You won with a score of "+myScore+". Rank: "+(rank+1);
			}else
			{
				gameComplete.text = "You have won!";
			}
            topScore.AddScore("Player", myScore);

        }
        else if (gameState == "DEATH")
        {
            gameComplete.text = "You have died.";
        }
		score.text = "Score: " + myScore;

        StartCoroutine(End());
	}

    IEnumerator End ()
    {
        yield return new WaitForSeconds(3);
        //GetComponent<ResetGame>().Reset();
        //Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("HighScore");
        
    }
	
}
