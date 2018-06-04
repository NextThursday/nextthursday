using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

	private AudioSource[] audioSources;

	void Start(){
		audioSources = FindObjectsOfType<AudioSource>();
		foreach (AudioSource myAudio in audioSources)
			myAudio.volume = PlayerPrefs.GetFloat("Volume");
	}
}
