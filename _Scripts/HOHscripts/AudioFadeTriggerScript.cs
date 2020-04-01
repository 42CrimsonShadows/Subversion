using UnityEngine;

public class AudioFadeTriggerScript : MonoBehaviour
{
    public void FadeSoundIn()
    {
        AudioSource[] soundsInScene = FindObjectsOfType<AudioSource>();
        //for eaach audiosource in the scene, the source and fade time are passed to the public static method AudioFadeScript
        foreach (AudioSource sound in soundsInScene)
        {
            StartCoroutine(AudioFadeScript.FadeIn(sound, 1.5f));
        }
    }
    public void FadeSoundOut()
    {
        AudioSource[] soundsInScene = FindObjectsOfType<AudioSource>();
        //for eaach audiosource in the scene, the source and fade time are passed to the public static method AudioFadeScript
        foreach (AudioSource sound in soundsInScene)
        {
            StartCoroutine(AudioFadeScript.FadeOut(sound, 1.5f));
        }
    }
}
