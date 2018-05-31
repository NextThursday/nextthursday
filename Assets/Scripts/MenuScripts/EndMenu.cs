using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;
	public TextMesh gameDesc;
	public GameObject input;

	private string playerName;
	private int myScore;

	void Start () {

		input = GameObject.Find("InputField") as GameObject;
        input.SetActive(false);
		playerName = "Player";
        myScore = 0;

		bool waitForType = false;
		string gameState = PlayerPrefs.GetString("GameEndState");
		gameState = "WIN";
        if (gameState == "WIN")
        {
			TopScore topScore = new TopScore();
            myScore = PlayerPrefs.GetInt("GameScore");
			myScore = 80;
            int rank = topScore.GetRank(myScore);
            if (rank < 10)
            {
				gameDesc.text = "You won with a score of "+myScore+". Rank: "+(rank+1);
			}else
			{
				gameDesc.text = "You have won!";
			}

			waitForType = true;
			SetName();

        }
        else if (gameState == "DEATH")
        {
			gameDesc.text = "You have died.";
        }
		score.text = "Score: " + myScore;

        if (!waitForType)
			StartCoroutine(End());
	}

	public void SetName()
    {
		input.SetActive(true);
        input.GetComponent<InputField>().onEndEdit.AddListener(GetInput);
    }

    void GetInput(string myname)
    {
		TopScore topScore = new TopScore();
        playerName = myname;
		Debug.Log(playerName);
		topScore.AddScore(playerName, myScore);

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
