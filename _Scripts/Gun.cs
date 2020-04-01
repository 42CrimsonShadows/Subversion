using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour
{
    public float missileFireRate = 60f;
    public float machineGunFireRate = 0.2f;
    public bool missile1Ready = true;
    public bool missile2Ready = true;
    protected float lastMissile1FireTime;
    protected float lastMissile2FireTime;
    protected float lastMachineGun1FireTime;
    protected float lastMachineGun2FireTime;

    //Waepon Heat
    public float minOverheatValue = 0f;
    public float maxOverheatValue = 10f;
    public float overheatValue1 = 0f;
    public float overheatValue2 = 0f;
    public float gunOverheatSpeed = 20f; //overheat after X seconds of firing
    public float gunHeatRegenSpeed = 1f;
    public float overheatRegenSpeed = 1f;
    public bool gunShutdown1 = false;
    public bool gunShutdown2 = false;

    //Heat UI Text
    public Text heatTextL;
    public Text warningTextL;
    public Text heatTextR;
    public Text warningTextR;

    private const string PLAYER_TAG = "Player";

    //origins for the bullet raycast
    [SerializeField]
    private GameObject gun1;
    [SerializeField]
    private GameObject gun2;
    [SerializeField]
    private GameObject gunCenterPoint;
    [SerializeField]
    private GameObject missleSpawnPoint1;
    [SerializeField]
    private GameObject missleSpawnPoint2;
    [SerializeField]
    private GameObject missileprefab;

    private WeaponsGraphics currentGraphics;
    public GunEquipper equipperScript;

    [SerializeField]
    private LayerMask hitMask;

    public AudioSource[] gunSoundsL;
    public AudioSource[] gunSoundsR;
    public AudioSource[] missileSoundsL;
    public AudioSource[] missileSoundsR;

    // Use this for initialization
    void Start()
    {
        //setting last missile fire time to 60sec ago so can fire immidiately when starts
        lastMissile1FireTime = Time.time - 60;
        lastMissile2FireTime = Time.time - 60;
        //setting last gun fire time to 10sec ago so can fire immidiately when starts
        lastMachineGun1FireTime = Time.time - 10;
        lastMachineGun2FireTime = Time.time - 10;

        //turn off the warning canvases untill they are needed (starting with them disabled makes them harder to ref)
        warningTextL.enabled = false;
        warningTextR.enabled = false;

        if (gun1 == null || gun2 == null || gunCenterPoint == null)
        {
            Debug.LogError("Gun Script: Gun Fire Points not referenced!");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    [Command]
    protected void CmdFireMissile1()
    {
        //GetComponent<localPlayerController>().animator.SetBool("Firing", true);

        //instantiate the prefab
        GameObject missile = Instantiate(missileprefab);
        NetworkServer.Spawn(missile);
        //give it a position and rotation that equals the firespot
        missile.transform.position = missleSpawnPoint1.transform.position;
        missile.transform.rotation = missleSpawnPoint1.transform.rotation;
    }

    [Command]
    protected void CmdFireMissile2()
    {
        //GetComponent<localPlayerController>().animator.SetBool("Firing", true);

        //instantiate the prefab
        GameObject missile = Instantiate(missileprefab);
        NetworkServer.Spawn(missile);
        //give it a position and rotation that equals the firespot
        missile.transform.position = missleSpawnPoint2.transform.position;
        missile.transform.rotation = missleSpawnPoint2.transform.rotation;
    }

    //Shoot Method
    //Executed on the Client Side Only
    [Client]
    protected void FireMachineGun1()
    {
        //will not execute if you this is not the local player
        if (!isLocalPlayer)
        {
            return;
        }

        //We are shooting so call the OnShootMethod on the SeverSide
        CmdOnShoot1();

        RaycastHit _hit;
        if (Physics.Raycast(gun1.transform.position, gun1.transform.forward, out _hit, 300, hitMask))
        {
            //if we hit a player
            if (_hit.collider.tag == PLAYER_TAG)
            {
                //call the server Command and pass ID of hit Player
                CmdPlayerShot(_hit.collider.name);
            }

            Debug.Log("I hit " + _hit.collider.name);

            //if we hit something call the ServerSide CmdOnhit method and pass in the hit objects point and normal information
            CmdOnHit(_hit.point, _hit.normal);

            var health = _hit.collider.GetComponentInChildren<Health>();

            if (health != null)
            {
                health.RpcTakeDamage(1);
            }
        }

        GetComponent<localPlayerController>().animator.SetBool("Firing1", true);

        if (!IsInvoking("FireSound1") && !gunShutdown1)
        {
            InvokeRepeating("FireSound1", 0f, .2f);
        }
    }

    //Executed on the Client Side Only
    [Client]
    protected void FireMachineGun2()
    {
        //will not execute if you this is not the local player
        if (!isLocalPlayer)
        {
            return;
        }

        //We are shooting so call the OnShootMethod on the SeverSide
        CmdOnShoot2();

        RaycastHit _hit;
        if (Physics.Raycast(gun2.transform.position, gun2.transform.forward, out _hit, 300, hitMask))
        {
            //if we hit a player
            if (_hit.collider.tag == PLAYER_TAG)
            {
                //call the server Command and pass ID of hit Player
                CmdPlayerShot(_hit.collider.name);
            }

            //if we hit something call the ServerSide CmdOnhit method and pass in the hit objects point and normal information
            CmdOnHit(_hit.point, _hit.normal);

            var health = _hit.collider.GetComponentInChildren<Health>();

            if (health != null)
            {
                health.RpcTakeDamage(1);
            }
        }

        GetComponent<localPlayerController>().animator.SetBool("Firing2", true);

        if (!IsInvoking("FireSound2") && !gunShutdown2)
        {
            InvokeRepeating("FireSound2", 0f, .2f);
        }
    }


    //executed on the SeverSide
    [Command]
    void CmdPlayerShot(string _playerID)
    {
        //slow mthod of destroying a player
        //Destroy(GameObject.Find(_ID));
    }

    //called on the ServerSide when the player shoots
    [Command]
    void CmdOnShoot1()
    {
        RpcDoShootEffect1();
    }
    //called on the ServerSide when the player shoots
    [Command]
    void CmdOnShoot2()
    {
        RpcDoShootEffect2();
    }

    //call ServerSide when we hit something
    //receives hit point and normal of the surface
    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
    }

    //Is called on all client when we need to do a shoot effect
    [ClientRpc]
    void RpcDoShootEffect1()
    {
        GetComponent<WeaponsGraphics>().MuzzleFlash_L1.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_L2.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_L3.Play();
    }

    //Is called on all client when we need to do a shoot effect
    [ClientRpc]
    void RpcDoShootEffect2()
    {
        GetComponent<WeaponsGraphics>().MuzzleFlash_R1.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_R2.Play();
        GetComponent<WeaponsGraphics>().MuzzleFlash_R3.Play();
    }

    //is called on all clients andcan spawn in effects
    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        Instantiate(GetComponent<WeaponsGraphics>().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
    }

    protected void StopFire1()
    {
        //gunAnimator.SetBool("Firing", false);
        GetComponent<localPlayerController>().animator.SetBool("Firing1", false);
        CancelInvoke("FireSound1");

    }
    protected void StopFire2()
    {
        //gunAnimator.SetBool("Firing", false);
        GetComponent<localPlayerController>().animator.SetBool("Firing2", false);
        CancelInvoke("FireSound2");
    }
    //protected void StopFireAll()
    //{
    //    CancelInvoke("FireSound1");
    //    CancelInvoke("FireSound2");
    //}

    public void FireSound1()
    {
        //SoundManager.Instance.PlayOneShot(SoundManager.Instance.gunFire);
        //play one shot of the audiosource on the Left gun
        gunSoundsL[2].Play();
    }
    public void FireSound2()
    {
        //SoundManager.Instance.PlayOneShot(SoundManager.Instance.gunFire);
        //play one shot of the audiosource on the Right gun
        gunSoundsR[2].Play();
    }
}