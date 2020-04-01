using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    public AudioSource walkEffectAudio;
    public AudioClip Walk;


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("1") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("2"))
        {
            if (!IsInvoking("WalkSound"))
            {
                InvokeRepeating("WalkSound", .1f, .72f);
            }
        }
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("10"))
        {
            //GetComponent<Animator>().SetBool("isWalking", false);
            CancelInvoke("WalkSound");
        }
	}

    public void WalkSound()
    {
        walkEffectAudio.PlayOneShot(Walk);
    }

    private void DoneWalking()
    {
        GetComponent<Animator>().SetBool("isWalking", false);
    }
}
