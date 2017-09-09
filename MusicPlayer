using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip start;
	public AudioClip game;
	public AudioClip end;
	private AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = start; //This is needed because OnLevelWasLoaded doesn't get called at the very beginning
			music.loop = true;
			music.Play();
		}
		
	}

	//Sets the appropriate music track to each scene
	void OnLevelWasLoaded(int level){
		music.Stop ();
		switch(level){
			case 0: music.clip = start;
			break;
			case 1: music.clip = game;
			break;
			case 2: music.clip = end;
			break;
		}
		music.loop = true;
		music.Play();
	}
}
