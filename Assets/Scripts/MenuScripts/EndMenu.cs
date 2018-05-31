using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> master

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;
	public TextMesh gameDesc;
	public GameObject input;

<<<<<<< HEAD
	private string playerName;
	private int myScore;

	void Start () {

		input = GameObject.Find("InputField") as GameObject;
        input.SetActive(false);
		playerName = "Player";
        myScore = 0;

		bool waitForType = false;
		string gameState = PlayerPrefs.GetString("GameEndState");
		//gameState = "WIN";
=======
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
>>>>>>> master
        if (gameState == "WIN")
        {
			TopScore topScore = new TopScore();
            myScore = PlayerPrefs.GetInt("GameScore");
<<<<<<< HEAD
			//myScore = 180;
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
=======
            int rank = topScore.GetRank(myScore);
            if (rank < 10)
            {
				gameComplete.text = "You won with a score of "+myScore+". Rank: "+(rank+1);
			}else
			{
				gameComplete.text = "You have won!";
			}
            topScore.AddScore("Player", myScore);
>>>>>>> master

        }
        else if (gameState == "DEATH")
        {
			gameDesc.text = "You have died.";
        }
		score.text = "Score: " + myScore;

<<<<<<< HEAD
        if (!waitForType)
			StartCoroutine(End());
=======
        StartCoroutine(End());
>>>>>>> master
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
<<<<<<< HEAD
        yield return new WaitForSeconds(5);
=======
        yield return new WaitForSeconds(3);
>>>>>>> master
        //GetComponent<ResetGame>().Reset();
        //Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("HighScore");
        
    }
	
}
