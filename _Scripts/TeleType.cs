using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TeleType : MonoBehaviour
{
    [SerializeField]
    private GameObject currentText;
    [SerializeField]
    private GameObject nextText;

    private TextMeshPro newText;

    private MainMenu mainMenuScript;

    [Header("FactionText")]
    public GameObject xImperium1;
    public GameObject fusionCoSyn1;
    public GameObject ShadaCol1;
    public GameObject ZeroG1;
    public GameObject RenovoSys1;
    public GameObject GoldenSun1;

    [Header("FactionLogo")]
    public Image xImperiumLogo;
    public Image fusionCoSynLogo;
    public Image ShadaColLogo;
    public Image ZeroGLogo;
    public Image RenovoSysLogo;
    public Image GoldenSunLogo;

    IEnumerator Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Intro-Scene"))
        {

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel1status)
            {
                Debug.Log("Faction 1: X-Imperium Loaded");

                if (xImperiumLogo != null)
                {
                    if (xImperiumLogo.enabled == false)
                    {
                        xImperiumLogo.enabled = true;
                    }
                }
            }

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel2status)
            {
                Debug.Log("Faction 2: FusionCoSyndicate Loaded");
                if (fusionCoSyn1 != null)
                {
                    fusionCoSyn1.SetActive(true);
                }

                if (fusionCoSynLogo != null)
                {
                    if (fusionCoSynLogo.enabled == false)
                    {
                        fusionCoSynLogo.enabled = true;
                    }
                }

                if (xImperium1)
                {
                    xImperium1.SetActive(false);
                }
            }

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel3status)
            {
                Debug.Log("Faction 2: ShadaCollective Loaded");
                if (ShadaCol1 != null)
                {
                    ShadaCol1.SetActive(true);
                }

                if (ShadaColLogo != null)
                {
                    if (ShadaColLogo.enabled == false)
                    {
                        ShadaColLogo.enabled = true;
                    }
                }

                if (xImperium1)
                {
                    xImperium1.SetActive(false);
                }
            }

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel4status)
            {
                Debug.Log("Faction 2: Zero-G Loaded");
                if (ZeroG1 != null)
                {
                    ZeroG1.SetActive(true);
                }

                if (ZeroGLogo != null)
                {
                    if (ZeroGLogo.enabled == false)
                    {
                        ZeroGLogo.enabled = true;
                    }
                }

                if (xImperium1)
                {
                    xImperium1.SetActive(false);
                }
            }

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel5status)
            {
                Debug.Log("Faction 2: RenovoSystems Loaded");
                if (RenovoSys1 != null)
                {
                    RenovoSys1.SetActive(true);
                }

                if (RenovoSysLogo != null)
                {
                    if (RenovoSysLogo.enabled == false)
                    {
                        RenovoSysLogo.enabled = true;
                    }
                }

                if (xImperium1)
                {
                    xImperium1.SetActive(false);
                }
            }

            if (PlayerManager_S.Instance.gameObject.GetComponent<GunEquipper>().fSel6status)
            {
                Debug.Log("Faction 2: GoldenSun Loaded");
                if (GoldenSun1 != null)
                {
                    GoldenSun1.SetActive(true);
                }

                if (GoldenSunLogo != null)
                {
                    if (GoldenSunLogo.enabled == false)
                    {
                        GoldenSunLogo.enabled = true;
                    }
                }

                if (xImperium1)
                {
                    xImperium1.SetActive(false);
                }
            }
        }

        mainMenuScript = GameObject.Find("IntroTextScroll").gameObject.GetComponent<MainMenu>();

        //Get Ref to textMeshPro Component if one exists; Otherwise add one
        newText = gameObject.GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        int totalVisibleCharacters = newText.textInfo.characterCount; //get # of visible characters in text object
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            newText.maxVisibleCharacters = visibleCount; // How many characters should textMeshPro display?

            //Once the last character has been revealed, wait 5.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(4.0f);

                if (nextText != null)
                {
                    nextText.SetActive(true);
                    currentText.SetActive(false);
                }
                else
                {
                    if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Intro-Scene"))
                    {
                        mainMenuScript.StartGame();
                    }

                    if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SplashScene"))
                    {
                        GameObject.Find("CenterEyeAnchor").GetComponent<ScreenFade>().Fade(FadeType.Out);

                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    

                    yield return new WaitForSeconds(4.0f);
                }
                yield return new WaitForSeconds(0.01f);
            }

            counter += 1;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
