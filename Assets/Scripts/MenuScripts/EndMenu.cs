using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {

    public TextMesh score;
    public TextMesh gameComplete;
	public TextMesh gameDesc;
    public Text placeholder;
	public GameObject input;

	private string playerName;
	private int myScore;

    private bool entered = false;

    bool lostScore = false;


    void Start () {
        


        
        playerName = "Player";
        myScore = PlayerPrefs.GetInt("GameScore");
        
		string gameState = PlayerPrefs.GetString("GameEndState");
		TopScore topScore = new TopScore();
			//myScore = 180;
        int rank = topScore.GetRank(myScore) + 1;





        if (gameState == "DEATH")
        {
            gameComplete.text = "Game Over";
            gameComplete.color = Color.red;
            gameDesc.text = ":(";
            gameDesc.color = Color.red;
        } else
        {
            gameDesc.color = Color.green;
        }


        if (rank <= 10)
        {
            gameDesc.color = Color.green;
            if (rank <= 1)
            {
                gameDesc.text = "You are the top!";
            }
            else
            {
                gameDesc.text = "You are on the scoreboard!";
            }
            SetName();
        }
        else
        {

            lostScore = true;
            if (gameState == "WIN")
            {
                gameDesc.text = "Good job";
            }
            placeholder.text = "Click to continue";

        }
        
        score.text = "Score: " + myScore + " Rank: " + rank;
        
	}

	public void SetName()
    {
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


    private void Update()
    {
        if (lostScore && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("HighScore");
        }
    }

}
