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
		float mvolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 1.0f;
		audioSources = FindObjectsOfType<AudioSource>();
		foreach (AudioSource myAudio in audioSources){
			if (myAudio.tag == "Sound")
				myAudio.volume = svolume;
			else if (myAudio.tag == "Music")
				myAudio.volume = mvolume;
		}
	}
}
