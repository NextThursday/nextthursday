using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	//public AudioClip convinced;
	//public AudioClip getHurt;
	//public AudioClip spell;
	//public AudioClip speedUp;
	//public AudioClip present;
	//public AudioClip push;
	public AudioClip[] audioClips;
	public AudioClip[] musics;
	public AudioClip[] narrative;
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
		
	}


	GameObject speaker;
	GameObject speaker2;

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
			index = Random.Range(0, musics.Length - 1);
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
            volume *= Random.Range(0.6f, 1f);
            pitch *= Random.Range(0.8f, 1.2f);
        }
        audioSource.volume = volume;
        audioSource.pitch = pitch;
		audioSource.tag = "Sound";

        audioSource.PlayOneShot(audioClips[(int)sound]);

        float duration = audioClips[(int)sound].length;
        StartCoroutine(DestroySpeaker(duration, speaker));
    }

	public void PlayNarration(Narration narration) {
		if ((int)narration >= narrative.Length){
			Debug.Log("Can't find the narrative");
			return;
		}


		speaker2 = InstallSpeaker ();

		AudioSource audioSource = speaker2.GetComponent<AudioSource>();

		if (audioSource.isPlaying) {
			return;
		}

		float volume = PlayerPrefs.GetFloat("NarratorVolume");
		audioSource.tag = "Narrator";
		audioSource.volume = volume;
		audioSource.clip (narration);

		audioSource.Play()
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
