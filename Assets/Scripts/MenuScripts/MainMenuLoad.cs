using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoad : MonoBehaviour {

	public void StartGame(){
		SceneManager.LoadScene("MainScene");
	}

	public void Tutorial(){
		Debug.Log("This is tutorial!");
		PlayerPrefs.DeleteKey("RunTutorial");
		SceneManager.LoadScene("MainScene");
	}

	public void QuitGame(){
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
