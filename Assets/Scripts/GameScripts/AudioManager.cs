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


	GameObject speaker;

	public void AddSoundTo(GameObject obj, Sound sound, bool isRandom){
		speaker = Instantiate(speakerPrefab);
        speaker.transform.parent = obj.transform;

		AudioSource audioSource = speaker.GetComponent<AudioSource>();
		float volume = PlayerPrefs.GetFloat("Volume");
		float pitch = 1;
		if (isRandom){
			volume *= Random.Range(0.7f, 1.3f);
			pitch *= Random.Range(0.5f, 1.5f);
		}
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.PlayOneShot(audioClips[(int)sound]);

		float duration = audioClips[(int)sound].length;
		StartCoroutine(DestroySpeaker(duration, speaker));
	}

	IEnumerator DestroySpeaker(float duration, GameObject obj)
    {
        yield return new WaitForSeconds(duration);
		Destroy(obj);
    }
}
