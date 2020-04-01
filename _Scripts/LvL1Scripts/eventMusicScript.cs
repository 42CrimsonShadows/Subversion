using UnityEngine;
using UnityEngine.Networking;

public class eventMusicScript : NetworkBehaviour {

    AudioSource m_MyAudioSource;
    private bool eventTriggered = false;
    GameObject backSound;

    void Start()
    {
        backSound = GameObject.Find("BackgroundMusic");

        //if (backSound != null)
        //{
        //    Debug.Log("Found backgroundsound game object: " + backSound);
        //}
        //else
        //{
        //    Debug.Log("Could not find background sound gameobject");
        //}

        m_MyAudioSource = backSound.GetComponentInChildren<AudioSource>();

        //if(m_MyAudioSource != null)
        //{
        //    Debug.Log("Found audiosource " + m_MyAudioSource);
        //}
        //else
        //{
        //    Debug.Log("Could not find AudioSource");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Event Triggered");

        if (eventTriggered)
        {
            return;
        }

        if (other.GetComponent<localPlayerController>())
        {
            Debug.Log("Local Player Recognized");

            if (m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Stop();
                eventTriggered = true;
                Debug.Log(m_MyAudioSource + " is now stopped.");
            }

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            else
            {
                return;
            }

        }
    }
}
