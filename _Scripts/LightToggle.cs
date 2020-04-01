using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    [Header("Mech Lights")]
    public GameObject topSpot_L;
    public GameObject topSpot_R;
    public GameObject botSpot_L;
    public GameObject botSpot_R;
    public GameObject botRed_L;
    public GameObject botRed_R;

    private void Start()
    {
        this.topSpot_L.SetActive(false);
        this.topSpot_R.SetActive(false);
        this.botSpot_L.SetActive(false);
        this.botSpot_R.SetActive(false);
        this.botRed_L.SetActive(false);
        this.botRed_R.SetActive(false);
    }
    public void ToggleLightTopL()
    {
        if(topSpot_L != null)
        {
            if (topSpot_L.activeInHierarchy == true)
            {
                topSpot_L.SetActive(false);
            }
            else if (topSpot_L.activeInHierarchy == false)
            {
                topSpot_L.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to topSpot_L.");
        }
    }
    public void ToggleLightTopR()
    {
        if (topSpot_R != null)
        {
            if (topSpot_R.activeInHierarchy == true)
            {
                topSpot_R.SetActive(false);
            }
            else if (topSpot_R.activeInHierarchy == false)
            {
                topSpot_R.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to topSpot_R.");
        }
    }
    public void ToggleLightBotL()
    {
        if (botSpot_L != null)
        {
            if (botSpot_L.activeInHierarchy == true)
            {
                botSpot_L.SetActive(false);
            }
            else if (botSpot_L.activeInHierarchy == false)
            {
                botSpot_L.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to botSpot_L.");
        }
    }
    public void ToggleLightBotR()
    {
        if (botSpot_R != null)
        {
            if (botSpot_R.activeInHierarchy == true)
            {
                botSpot_R.SetActive(false);
            }
            else if (botSpot_R.activeInHierarchy == false)
            {
                botSpot_R.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to botSpot_R.");
        }
    }
    public void ToggleLightRedL()
    {
        if (botRed_L != null)
        {
            if (botRed_L.activeInHierarchy == true)
            {
                botRed_L.SetActive(false);
            }
            else if (botRed_R.activeInHierarchy == false)
            {
                botRed_L.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to botRed_L.");
        }
    }
    public void ToggleLightRedR()
    {
        if (botRed_R != null)
        {
            if (botRed_R.activeInHierarchy == true)
            {
                botRed_R.SetActive(false);
            }
            else if (botRed_R.activeInHierarchy == false)
            {
                botRed_R.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Can't find reference to botRed_R.");
        }
    }
}
