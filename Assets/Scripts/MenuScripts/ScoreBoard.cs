using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{

	public TextMesh myText;
	private TopScore topScore;

	// Use this for initialization
	void Start()
	{
		topScore = new TopScore();
		myText.text = topScore.ToString();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<ResetGame>().Reset();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
