using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour {

    //public static so can be ref from anywhee
    public static VoiceManager aiInstance = null;

    private AudioSource voiceAudio;

    public AudioClip ivy1Briefing;
    public AudioClip ivy2Hostiles;
    public AudioClip ivy3Enemies;
    public AudioClip ivy4Caution;
    public AudioClip ivy5Remains;
    public AudioClip ivy6OnYour6;
    public AudioClip ivy7SecLeft;
    public AudioClip ivy8Return;

    void Start () {
        //Sound Manager Singleton*************
        //make sure there is only ever one copy of this gameobject in existence
        if (aiInstance == null)
        {
            aiInstance = this;
        }
        else if (aiInstance != null)
        {
            Destroy(gameObject);
        }

        //persist througout the game
        DontDestroyOnLoad(aiInstance);
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
                voiceAudio = source;
            }
        }
    }

    //wrapper called to play a sound
    public void PlayOneShot(AudioClip clip)
    {
        voiceAudio.clip = clip;
        voiceAudio.Play();
    }
}
