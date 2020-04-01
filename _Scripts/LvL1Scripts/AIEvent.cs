using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AIEvent : MonoBehaviour {

    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject trigger4;
    public GameObject trigger5;
    public GameObject trigger6;
    public GameObject trigger7;

    public GameObject indicator;

    private GameObject activeCamera;

    private GameObject aiScreen1;
    public Playback aiPlayback;
    private Animator talkingStatus;

    //private string aiFaceSeq1 = "Mission-Brief_RSQ";
    private int aiFaceSeq1Length = 27;
    private string aiFaceSeq2 = "Mult-H-Detected_RSQ";
    private int aiFaceSeq2Length = 3;
    private string aiFaceSeq3 = "E-H-Detected_RSQ";
    private int aiFaceSeq3Length = 2;
    private string aiFaceSeq4 = "Detecting-Beacon_RSQ";
    private int aiFaceSeq4Length = 14;
    private string aiFaceSeq5 = "EF-Spotted_RSQ";
    private int aiFaceSeq5Length = 24;
    private string aiFaceSeq6 = "On-Your-6_RSQ";
    private int aiFaceSeq6Length = 5;
    private string aiFaceSeq7 = "Fifteen-Sec_RQS";
    private int aiFaceSeq7Length = 4;
    private string aiFaceSeq8 = "Return-To-Base_RSQ";

    public bool aiDialog1 = false;
    public bool aiDialog2 = false;
    public bool aiDialog3 = false;
    public bool aiDialog4 = false;
    public bool aiDialog5 = false;
    public bool aiDialog6 = false;
    public bool aiDialog7 = false;
    public bool aiDialog8 = false;

    void Start()
    {
        activeCamera = GameObject.Find("CenterEyeAnchor");
        if (activeCamera == null)
        {
            Debug.Log("CenterEyeAnchor not found");
        }

        aiScreen1 = GameObject.Find("RawAIImageL");
        if (aiScreen1 == null)
        {
            Debug.Log("AIinterfaceL not found.");
        }
        else
        {
            Debug.Log("AIinterfaceL found.");

            aiPlayback.uiOutList.Add(aiScreen1.GetComponent<RawImage>());
            aiPlayback.rawImage = aiScreen1.GetComponent<RawImage>();

            if(aiPlayback.rawImage == null)
            {
                Debug.Log("Couldn't find the raw image from RawAIImageL.");
            }
        }

        //get a ref to the animator on the AIInterfaceL gameobject
        talkingStatus = GameObject.FindWithTag("AIScreen").GetComponent<Animator>();
    }

    void Update()
    {
        if (aiPlayback.IsPlaying == false)
        {
            talkingStatus.SetBool("isTalking", false);
        }
        else
        {
            talkingStatus.SetBool("isTalking", true);
        }

        //if isPlaying is false and trigger1 eqauls true
        if (aiPlayback.IsPlaying == false)
        {

        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<Collider>() == trigger1 && aiDialog1 == false)
        {
            Debug.Log("trigger one set off.");
            aiDialog1 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy1Briefing);
        }
        if (this.GetComponent<Collider>() == trigger2 && aiDialog2 == false)
        {
            aiDialog2 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy2Hostiles);
        }
        if (this.GetComponent<Collider>() == trigger3 && aiDialog3 == false)
        {
            aiDialog3 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy3Enemies);
        }
        if (this.GetComponent<Collider>() == trigger4 && aiDialog4 == false)
        {
            aiDialog4 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy4Caution);
        }
        if (this.GetComponent<Collider>() == trigger5 && aiDialog5 == false)
        {
            aiDialog5 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy5Remains);
            indicator.SetActive(true);
        }
        if (this.GetComponent<Collider>() == trigger6 && aiDialog6 == false)
        {
            aiDialog6 = true;
            VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy6OnYour6);
            StartCountdown1();
        }
        if (this.GetComponent<Collider>() == trigger6 && aiDialog6 == true)
        {
            SceneManager.LoadScene("HoH-ComingSoon");
        }
    }

    private void StartCountdown1()
    {
        StartCoroutine("Countdown1");
    }

    IEnumerator Countdown1()
    {
        yield return new WaitForSeconds(5);



        yield return new WaitForSeconds(15);
        //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy7SecLeft);
        aiPlayback.clip = VoiceManager.aiInstance.ivy7SecLeft;
        aiPlayback.LoadSequence(resourceFolder: aiFaceSeq7, true);
        StartCoroutine("Countdown2");
    }
    IEnumerator Countdown2()
    {
        yield return new WaitForSeconds(4);

        //open the AI window
        //talkingStatus.SetBool("isTalking", false);

        yield return new WaitForSeconds(15);
        //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy8Return);
        indicator.SetActive(false);
        aiPlayback.clip = VoiceManager.aiInstance.ivy8Return;
        aiPlayback.LoadSequence(resourceFolder: aiFaceSeq7, true);
        StartCoroutine("MassAttack");
    }

    IEnumerator MassAttack()
    {
        yield return new WaitForSeconds(5);

        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("EnemyE-Frame");
        foreach(GameObject enemy in enemyArray)
        {
            Debug.Log("RED ALERT RED ALERT GO GO GO");
            enemy.GetComponent<EnemyEframe>().visualRange = 500;
            enemy.GetComponent<EnemyEframe>().chaseRange = 500;
            enemy.GetComponent<EnemyEframe>().attackRange = 120;
        }
    }

    public void ObectTriggered(GameObject trigger)
    {
        if(trigger == trigger1 && aiDialog1 == false)
        {
            Debug.Log("trigger one set off.");
            aiDialog1 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy1Briefing);
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            //pass in the next vars for the next ai trigger wall
            StartCoroutine(LoadNextAIInteraction(VoiceManager.aiInstance.ivy2Hostiles, aiFaceSeq2, aiFaceSeq1Length));
        }
        if (trigger == trigger2 && aiDialog2 == false)
        {
            aiDialog2 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy2Hostiles);

            //open the AI window
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            //pass in the next vars for the next ai trigger wall
            StartCoroutine(LoadNextAIInteraction(VoiceManager.aiInstance.ivy3Enemies, aiFaceSeq3, aiFaceSeq2Length));
                
            //LoadNextAIInteraction(VoiceManager.aiInstance.ivy3Enemies, aiFaceSeq3);
        }
        if (trigger == trigger3 && aiDialog3 == false)
        {
            aiDialog3 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy3Enemies);

            //open the AI window
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            //pass in the next vars for the next ai trigger wall
            StartCoroutine(LoadNextAIInteraction(VoiceManager.aiInstance.ivy4Caution, aiFaceSeq4, aiFaceSeq3Length));

        }
        if (trigger == trigger4 && aiDialog4 == false)
        {
            aiDialog4 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy4Caution);

            //open the AI window
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            //pass in the next vars for the next ai trigger wall
            StartCoroutine(LoadNextAIInteraction(VoiceManager.aiInstance.ivy5Remains, aiFaceSeq5, aiFaceSeq4Length));
        }
        if (trigger == trigger5 && aiDialog5 == false)
        {
            aiDialog5 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy5Remains);
            indicator.SetActive(true);

            //open the AI window
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            //pass in the next vars for the next ai trigger wall
            StartCoroutine(LoadNextAIInteraction(VoiceManager.aiInstance.ivy6OnYour6, aiFaceSeq6, aiFaceSeq5Length));
        }
        if (trigger == trigger6 && aiDialog6 == false)
        {
            aiDialog6 = true;
            //VoiceManager.aiInstance.PlayOneShot(VoiceManager.aiInstance.ivy6OnYour6);

            //open the AI window
            //talkingStatus.SetBool("isTalking", true);
            aiPlayback.Play();
            StartCountdown1();
        }
        if (trigger == trigger7 && aiDialog6 == true)
        {
            activeCamera.GetComponent<ScreenFade>().Fade(FadeType.Out);
            StartCoroutine(FadeToNewScene("HoH-ComingSoon"));
        }

    }

    // Fades alpha from 1.0 to 0.0
    IEnumerator FadeToNewScene(string scene)
    {
        AudioSource[] soundsInScene = FindObjectsOfType<AudioSource>();
        //for eaach audiosource in the scene, the source and fade time are passed to the public static method AudioFadeScript
        foreach (AudioSource sound in soundsInScene)
        {
            StartCoroutine(AudioFadeScript.FadeOut(sound, 1.5f));
        }
        yield return new WaitForSeconds(2.99f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator LoadNextAIInteraction(AudioClip soundClip, string faceClip, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //close the Ai ScreenGroup
        //talkingStatus.SetBool("isTalking", false);
        //set a new string for the seq folder
        aiPlayback.sequenceFolder = faceClip;
        //load the seq folder in async load, but don't play yet
        aiPlayback.LoadSequence(resourceFolder: faceClip);
        //load a new sound clip
        aiPlayback.clip = soundClip;

    }
}
