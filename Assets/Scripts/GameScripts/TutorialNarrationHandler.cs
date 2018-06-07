using UnityEngine;

public class TutorialNarrationHandler : MonoBehaviour {
	public AudioClip narration;
	public bool alreadyPlayed = false;

	void Start() {}

	void onTriggerEnter() {
		if (!alreadyPlayed) {
			AudioManager audioManager = FindObjectOfType<AudioManager> ();
			audioManager.PlayTutorialNarrative (AudioManager.Narration.CORRIDOR_LEVEL);
		}
	}
}
