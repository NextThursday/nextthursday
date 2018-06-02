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
	private int passedLevel;

	void Start () {
        
        input.SetActive(false);
        playerName = "Player";
        myScore = 0;
		passedLevel = 0;
        
		TopScore topScore = new TopScore();
        myScore = PlayerPrefs.GetInt("GameScore");
        passedLevel = PlayerPrefs.GetInt("LevelLoad");
		int rank = topScore.GetRank(myScore);

        bool waitForType = false;
		string gameState = PlayerPrefs.GetString("GameEndState");
		//gameState = "WIN";
        if (gameState == "WIN")
        {
            Debug.Log("win!!");
			gameDesc.text = "You have won!";
			passedLevel++;
        }
        else if (gameState == "DEATH")
        {
            Debug.Log("death");
            gameDesc.text = "You have died.";
        }

        if (rank < 10)
        {
            gameDesc.text += "\nYou got a score of " + myScore + ". Rank: " + (rank + 1);
			waitForType = true;
            SetName();
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
        topScore.AddScore(playerName, myScore, passedLevel);
        SceneManager.LoadScene("HighScore");
    }

    IEnumerator End ()
    {
        yield return new WaitForSeconds(5);
		SceneManager.LoadScene("HighScore");
        
    }
	
}
