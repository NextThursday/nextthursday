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

    private bool entered = false;

	void Start () {
        
        input.SetActive(false);
        playerName = "Player";
        myScore = 0;
        
        bool waitForType = false;
		string gameState = PlayerPrefs.GetString("GameEndState");
		//gameState = "WIN";
        if (gameState == "WIN")
        {
            Debug.Log("win!!");
			TopScore topScore = new TopScore();
            myScore = PlayerPrefs.GetInt("GameScore");
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

        }
        else if (gameState == "DEATH")
        {
            Debug.Log("death");
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
        if (!entered)
        {
            TopScore topScore = new TopScore();
            playerName = myname;
            Debug.Log(playerName);
            topScore.AddScore(playerName, myScore);
            entered = true;
            SceneManager.LoadScene("HighScore");
        }
    }

    IEnumerator End ()
    {
        yield return new WaitForSeconds(5);
		SceneManager.LoadScene("HighScore");
        
    }
	
}
