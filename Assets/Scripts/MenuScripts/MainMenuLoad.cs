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
		if (PlayerPrefs.HasKey("SoundVolume")){
			soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
		}else{
			soundVolumeSlider.value = 1;
			PlayerPrefs.SetFloat("SoundVolume", 1f);
		}

		if (PlayerPrefs.HasKey("MusicVolume")){
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }else{
            musicVolumeSlider.value = 1;
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }

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
