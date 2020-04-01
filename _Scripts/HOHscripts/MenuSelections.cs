using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelections : MonoBehaviour {

    public Image btnColor;
    public Color regColor;
    public Color overColor;

    //called when the begin button is pressed on the menu screnn
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

    public void ReturnToQuarters()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.selectNormal);
        SceneManager.LoadScene("HoH-Quarters");
    }

    public void BeginMission()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.selectNormal);
        SceneManager.LoadScene("HoHL-Mission1");
    }

    private void OnGazeEnter()
    {
        btnColor.color = overColor;
    }

    private void OnGazeExit()
    {
        btnColor.color = regColor;
    }
}
