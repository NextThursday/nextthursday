using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoad : MonoBehaviour {
    
	public Slider volumeSlider;

	void Start()
	{
		volumeSlider.value = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 1;
	}
	public void StartGame(){
		SceneManager.LoadScene("MainScene");
	}

	public void Tutorial(){
		Debug.Log("This is tutorial!");
		PlayerPrefs.DeleteKey("RunTutorial");
		SceneManager.LoadScene("MainScene");
	}

	public void SetVolume(float volume){
		PlayerPrefs.SetFloat("Volume", volume);
	}

	public void QuitGame(){
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
