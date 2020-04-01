using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundManager : NetworkBehaviour {

	//public static so can be ref from anywhee
	public static SoundManager Instance = null;

	private AudioSource soundEffectAudio;

	public AudioClip gunFire;
    public AudioClip gunFireHits;
	//public AudioClip Walk;
    public AudioClip missileFire;
    public AudioClip explosions;
    public AudioClip landingBoom;
    public AudioClip computerVoice;
    public AudioClip teamVoice;
    public AudioClip gearMovement;
    public AudioClip selectNormal;


	public void Start () {
        //Sound Manager Singleton*************
		//make sure there is only ever one copy of this gameobject in existence
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != null)
		{
			Destroy(gameObject);
		}

		//persist througout the game
		DontDestroyOnLoad(Instance);
        //**************************************

		//make an array called sources for the AudioSource components
		AudioSource[] sources = GetComponents<AudioSource>();

		//for each in the sources array
		foreach (AudioSource source in sources)
		{
			//if the source is empty...
			if (source.clip == null)
			{
				//this is the audiosource we are looking for and we will call it soundEffectAudio
				soundEffectAudio = source;
			}
		}
	}

	//wrapper called to play a sound
	public void PlayOneShot(AudioClip clip)
	{
		//plays sound passed in from another script
		soundEffectAudio.PlayOneShot(clip);
	}
}
