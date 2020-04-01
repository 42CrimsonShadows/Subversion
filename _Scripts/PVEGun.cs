using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PVEGun : NetworkBehaviour {

    public LayerMask hitMask;
    public GameObject pveTagetPoint;
    public AudioSource pveGunSound;
    public AudioClip pveMachineGunSound;

    public float pveMissileFireRate = 120f;
    public float pveMachineGunFireRate = 0.4f;
    public float pveLastMissileFireTime;
    public float pveLastMachineGunFireTime;

    private WeaponsGraphics currentGraphics;
    private Health targetHealth;

    // Use this for initialization
    void Start () {
        pveLastMissileFireTime = Time.time - 60;
        pveLastMachineGunFireTime = Time.time - 10;
        pveGunSound = GetComponent<AudioSource>();

        if (pveTagetPoint == null)
        {
            Debug.LogError("PVEGun Script: Gun Fire Point not referenced!");
            this.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [Client]
    public void PVEFireMachineGun()
    {
        CmdPVEOnShoot();

        RaycastHit _hit;
        if(Physics.Raycast(pveTagetPoint.transform.position, pveTagetPoint.transform.forward, out _hit, 300, hitMask))
        {
            CmdPVEOnHit(_hit.point, _hit.normal);

            targetHealth = _hit.collider.GetComponentInParent<Health>();

            if (targetHealth == null)
            {
                targetHealth = _hit.collider.GetComponentInParent<Health>();
            }

            if (targetHealth != null)
            {
                targetHealth.RpcTakeDamage(1);
            }
        }

        GetComponent<EnemyEframe>().pveAnimator.SetBool("Firing", true);

        if (!IsInvoking("PVEFireSound"))
        {
            InvokeRepeating("PVEFireSound", 0f, .4f);
        }
    }

    [Command]
    private void CmdPVEOnShoot()
    {
        RpcPVEDoShootEffect();
    }

    [Command]
    private void CmdPVEOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcPVEDoHitEffect(_pos, _normal);
    }
    [ClientRpc]
    private void RpcPVEDoShootEffect()
    {
        GetComponent<WeaponsGraphics>().MuzzleFlash_L1.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_L2.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_L3.Play();

        GetComponent<WeaponsGraphics>().MuzzleFlash_R1.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_R2.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_R3.Play();
    }
    [ClientRpc]
    private void RpcPVEDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        Instantiate(GetComponent<WeaponsGraphics>().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));        
    }

    public void PVEFireSound()
    {
        //SoundManager.Instance.PlayOneShot(SoundManager.Instance.gunFire);
        //second umber scales the audio valumn
        pveGunSound.PlayOneShot(pveMachineGunSound, .9f);
    }

    public void PVEStopFire()
    {
        //gunAnimator.SetBool("Firing", false);
        GetComponent<EnemyEframe>().pveAnimator.SetBool("Firing", false);
        CancelInvoke("PVEFireSound");
    }
}
