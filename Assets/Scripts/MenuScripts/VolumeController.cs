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
		float volume = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 1.0f;
		audioSources = FindObjectsOfType<AudioSource>();
		foreach (AudioSource myAudio in audioSources)
            myAudio.volume = volume;
	}
}
