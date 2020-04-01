using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour {

    public Image imageColor;
    public Color normalColor;
    public Color highlightColor;
    private GameObject activeCamera;

    public bool factionSelection1 = false;
    public bool factionSelection2 = false;
    public bool factionSelection3 = false;
    public bool factionSelection4 = false;
    public bool factionSelection5 = false;
    public bool factionSelection6 = false;

    public bool frameSelection1 = false;
    public bool frameSelection2 = false;
    public bool frameSelection3 = false;

    public bool gunSelection1 = false;
    public bool gunSelection2 = false;
    public bool gunSelection3 = false;

    public bool missileSelection1 = false;
    public bool missileSelection2 = false;
    public bool missileSelection3 = false;

    public bool shieldSelection1 = false;
    public bool shieldSelection2 = false;
    public bool shieldSelection3 = false;

    public bool engineSelection1 = false;
    public bool engineSelection2 = false;
    public bool engineSelection3 = false;

    public bool legSelection1 = false;
    public bool legSelection2 = false;
    public bool legSelection3 = false;

    public bool missionSelection1_1 = false;
    public bool missionSelection2_1 = false;
    public bool missionSelection3_1 = false;

    private void Start()
    {
        activeCamera = GameObject.Find("CenterEyeAnchor");

        if(activeCamera == null)
        {
            Debug.Log("CenterEyeAnchor not found");
        }
    }

    public void FactionSelection()
    {

    }

    public void SoundSelection()
    {

    }

    public void GunSelection()
    {

    }

    public void MissileSelection()
    {

    }

    public void ShieldSelection()
    {

    }

    public void EngineSelection()
    {

    }

    public void LegSelection()
    {

    }

    public void FrameSelection()
    {

    }

    public void MissionSelection()
    {

    }

    public void StartMenu()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("HoH-StartMenu"));
    }


    //called when the begin button is pressed on the menu screnn
    public void StartGame()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("HoH-Quarters"));
    }

    public void StartIntro()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("Intro-Scene"));
    }

    //quites the game when the quit button is pressed on the main meny screen
    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToQuarters()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("HoH-Quarters"));
    }

    public void BeginMission()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("lobbyScene"));
    }

    public void GoToDock()
    {
        activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
        StartCoroutine(FadeToNewScene("HoH-LoadingDocks"));
    }

    public void OnGazeEnter()
    {
        imageColor.color = highlightColor;
    }

    public void OnGazeExit()
    {
        imageColor.color = normalColor;
        //Debug.Log("normalized");
    }

    // Fades alpha from 1.0 to 0.0
    IEnumerator FadeToNewScene(string scene)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.selectNormal);
        }

        //fade out the sounds in the scene//////////////////
        //assign all the audiosources in the scene to an array
        AudioSource[] soundsInScene = FindObjectsOfType<AudioSource>();
        //for eaach audiosource in the scene, the source and fade time are passed to the public static method AudioFadeScript
        foreach(AudioSource sound in soundsInScene)
        {
            StartCoroutine(AudioFadeScript.FadeOut(sound, 1.5f));
        }
        
        yield return new WaitForSeconds(2.99f);
        SceneManager.LoadScene(scene);
    }
}
