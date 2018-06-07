using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoad : MonoBehaviour {
    
	public Slider soundVolumeSlider;
	public Slider musicVolumeSlider;
	public Button[] buttons;

	private AudioManager audioManager;

	void Start()
	{
		soundVolumeSlider.value = PlayerPrefs.HasKey("SoundVolume") ? PlayerPrefs.GetFloat("SoundVolume") : 1;
		musicVolumeSlider.value = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 1;
		audioManager = FindObjectOfType<AudioManager>();
		GetComponent<ResetGame>().Reset();
	}

	public void StartGame(){
		SceneManager.LoadScene("MainScene");
	}

	public void Tutorial(){
		Debug.Log("This is tutorial!");
		PlayerPrefs.DeleteKey("SkipTutorial");
		SceneManager.LoadScene("MainScene");
	}

	public void SetSoundVolume(float volume){
		PlayerPrefs.SetFloat("SoundVolume", volume);
		VolumeController volumeController = FindObjectOfType<VolumeController>();
		volumeController.ResetVolume();
	}
    
	public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        VolumeController volumeController = FindObjectOfType<VolumeController>();
        volumeController.ResetVolume();
    }

	public void OnMouseOver()
	{
		//audioManager.AddSoundTo(AudioManager.Sound.HURT);
	}

	public void OnMouseDown()
	{
		//audioManager.AddSoundTo(AudioManager.Sound.SPELL);
	}

	public void QuitGame(){
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
