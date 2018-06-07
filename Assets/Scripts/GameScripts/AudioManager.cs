using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public class TutorialNarrative {
		public AudioClip audio;
		public DateTime time;

		public TutorialNarrative(AudioClip audio, DateTime time) {
			this.audio = audio;
			this.time = time;
		}
	}

	public static string NARRATOR = "Narrator";

	public AudioClip[] audioClips;
	public AudioClip[] musics;
	public AudioClip[] narrative;
	public List<TutorialNarrative> tutorialNarrative = new List<TutorialNarrative>();
	public AudioClip currentNarrative;
	public GameObject speakerPrefab;

	public enum Sound
    {
        CONVINCED,
        HURT,
        SPELL,
        SPEEDUP,
        PRESENT,
        PUSH
    };

	public enum Music
	{
        TUTORIAL,
        GAMEPLAY,
        SCOREBOARD
	};

	public enum Narration {
		TUTORIAL,
		LEVEL_ONE,
		WATER_LEVEL,
		PORTAL_LEVEL,
		CORRIDOR_LEVEL,
		INCINERATOR_LEVEL
	}


	GameObject speaker;

	public void PlayMusic(Music music){
		PlayMusic(music, false);
	}

	public void PlayMusic()
    {
		PlayMusic(Music.GAMEPLAY, true);
    }

	private void PlayMusic(Music music, bool isRandom){
		if ((int)music >= musics.Length){
			Debug.Log("Can't find the music");
			return;
		}

		speaker = InstallSpeaker();

		AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("MusicVolume");
		audioSource.tag = "Music";
        audioSource.volume = volume;
		audioSource.loop = true;

		int index;
		if (isRandom)
			index = UnityEngine.Random.Range(0, musics.Length - 1);
		else
			index = (int)music;
		
		audioSource.clip = musics[index];
		audioSource.Play();
	}

	public void AddSoundTo(Sound sound)
    {
        AddSoundTo(sound, false);
    }

    public void AddSoundTo(Sound sound, bool isRandom)
    {
        AddSoundTo(gameObject, sound, isRandom);
    }

    public void AddSoundTo(GameObject obj, Sound sound, bool isRandom)
    {
		if ((int)sound >= audioClips.Length){
			Debug.Log("Can't find the sound");
			return;
		}

        speaker = InstallSpeaker(obj);

        AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("SoundVolume");
        float pitch = 1;
        if (isRandom)
        {
			volume *= UnityEngine.Random.Range(0.6f, 1f);
			pitch *= UnityEngine.Random.Range(0.8f, 1.2f);
        }
        audioSource.volume = volume;
        audioSource.pitch = pitch;
		audioSource.tag = "Sound";

        audioSource.PlayOneShot(audioClips[(int)sound]);

        float duration = audioClips[(int)sound].length;
        StartCoroutine(DestroySpeaker(duration, speaker));
    }



	public void PlayTutorialNarrative(Narration narration) {
		if ((int)narration >= narrative.Length){
			Debug.Log("Can't find the narrative");
			return;
		}

		speaker = InstallSpeaker ();

		AudioSource audioSource = speaker.GetComponent<AudioSource>();

		float volume = PlayerPrefs.GetFloat(MainMenuLoad.NARRATOR_VOLUME);
		audioSource.tag = NARRATOR;
		audioSource.volume = volume;
		audioSource.clip = narrative [(int)narration];

		addToTutorialNarrative (narrative [(int)narration]);

		if (tutorialNarrative.Count > 0 && DateTime.Now < tutorialNarrative [tutorialNarrative.Count - 1].time  ) {
			float delay = (float)(DateTime.Now - tutorialNarrative [tutorialNarrative.Count - 1].time).TotalMilliseconds;
			audioSource.PlayDelayed (delay);
		} else {
			audioSource.Play ();
		}
	}

	public void addToTutorialNarrative (AudioClip narration) {
		tutorialNarrative.Add (new TutorialNarrative(narration, DateTime.Now.AddMilliseconds(narration.length)));
	}

	public void PlayNarration() {
		int level = PlayerPrefs.GetInt("LevelLoad");
		Narration narration;

		switch (level) {
			case 1:
				narration = Narration.CORRIDOR_LEVEL;
				break;
			case 2:
				narration = Narration.WATER_LEVEL;
				break;
			case 3:
				narration = Narration.INCINERATOR_LEVEL;
				break;
			case 4:
				narration = Narration.PORTAL_LEVEL;
				break;
			case 0:
			default:
				narration = Narration.LEVEL_ONE;
				break;
		}

		speaker = InstallSpeaker ();

		AudioSource audioSource = speaker.GetComponent<AudioSource>();

		float volume = PlayerPrefs.GetFloat(MainMenuLoad.NARRATOR_VOLUME);
		audioSource.tag = NARRATOR;
		audioSource.volume = volume;

		audioSource.PlayOneShot (narrative[(int)narration]);
	}

	private GameObject InstallSpeaker(GameObject obj)
    {
        GameObject newSpeaker = Instantiate(speakerPrefab);
		newSpeaker.transform.position = obj.transform.position;
		return newSpeaker;
    }

    private GameObject InstallSpeaker()
    {
        return InstallSpeaker(gameObject);
    }

	IEnumerator DestroySpeaker(float duration, GameObject obj)
    {
        yield return new WaitForSeconds(duration);
		Destroy(obj);
    }
}
