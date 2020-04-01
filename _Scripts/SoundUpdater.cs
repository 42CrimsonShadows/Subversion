using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundUpdater : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetMasterVol(float value)
    {
        //pass in the slider value as a logarithmic value for the mixers logarithmic scale
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
    public void SetMusicVol(float value)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        //masterMixer.SetFloat("MusicVolume", value);
    }
    public void SetAmbVol(float value)
    {
        masterMixer.SetFloat("AmbienceVolume", Mathf.Log10(value) * 20);
    }
    public void SetGunVol(float value)
    {
        masterMixer.SetFloat("GunVolume", Mathf.Log10(value) * 20);
    }
    public void SetAIVol(float value)
    {
        masterMixer.SetFloat("AIVolume", Mathf.Log10(value) * 20);
    }
    public void SetSquadVol(float value)
    {
        masterMixer.SetFloat("SquadVolume", Mathf.Log10(value) * 20);
    }
    public void SetExploVol(float value)
    {
        masterMixer.SetFloat("ExplosionVolume", Mathf.Log10(value) * 20);
    }
}
