using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

	private AudioSource[] audioSources;

	void Start(){
		ResetVolume();
	}

	public void ResetVolume(){
		float svolume = PlayerPrefs.HasKey("SoundVolume") ? PlayerPrefs.GetFloat("SoundVolume") : 1.0f;
		float mvolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 0.5f;
		float nvolume = PlayerPrefs.HasKey (MainMenuLoad.NARRATOR_VOLUME) ? PlayerPrefs.GetFloat (MainMenuLoad.NARRATOR_VOLUME) : 1f;
		audioSources = FindObjectsOfType<AudioSource>();
		foreach (AudioSource myAudio in audioSources){
			if (myAudio.tag == "Sound")
				myAudio.volume = svolume;
			else if (myAudio.tag == "Music")
				myAudio.volume = mvolume;
			else if (myAudio.tag == AudioManager.NARRATOR)
				myAudio.volume = nvolume;
		}
	}
}
