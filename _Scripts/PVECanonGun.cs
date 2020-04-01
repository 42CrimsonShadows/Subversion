using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PVECanonGun : NetworkBehaviour
{
    public LayerMask turretHitMask;

    [SerializeField]
    private GameObject pveTurretTargetPoint;

    public AudioSource pveTurretGunSound;
    //public AudioClip pveTurretMachineGunSound;

    public float pveTurretMissileFireRate = 120f;
    public float pveTurretMachineGunFireRate = 0.4f;
    public float pveLastTurretMissileFireTime;
    public float pveLastTurretMachineGunFireTime;

    private WeaponsGraphics currentGraphics;
    private Health targetHealth;

    // Use this for initialization
    void Start()
    {
        pveLastTurretMissileFireTime = Time.time - 60;
        pveLastTurretMachineGunFireTime = Time.time - 10;
        pveTurretGunSound = GetComponent<AudioSource>();

        if (pveTurretTargetPoint == null)
        {
            Debug.LogError("PVECanonGun Script: Gun Fire Point not referenced!");
            //this.enabled = false;
        }
    }

    [Client]
    public void PVETurretFireCanon()
    {
        CmdTurretPVEOnShoot();

        RaycastHit _hit;
        if (Physics.Raycast(pveTurretTargetPoint.transform.position, pveTurretTargetPoint.transform.forward, out _hit, 300, turretHitMask))
        {
            CmdTurretPVEOnHit(_hit.point, _hit.normal);

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

        if (!IsInvoking("PVETurretFireSound"))
        {
            InvokeRepeating("PVETurretFireSound", 0f, .2f);
        }
    }

    [Command]
    private void CmdTurretPVEOnShoot()
    {
        RpcTurretPVEDoShootEffect();
    }

    [Command]
    private void CmdTurretPVEOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcTurretPVEDoHitEffect(_pos, _normal);
    }
    [ClientRpc]
    private void RpcTurretPVEDoShootEffect()
    {
        GetComponent<WeaponsGraphics>().MuzzleFlash_L1.Play();
        //GetComponent<WeaponsGraphics>().MuzzleFlash_L2.Play();
        StartCoroutine(TurretPVEDoShootEffect2());
    }

    //[ClientRpc]
    private IEnumerator TurretPVEDoShootEffect2()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<WeaponsGraphics>().MuzzleFlash_L2.Play();
    }

    [ClientRpc]
    private void RpcTurretPVEDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        Instantiate(GetComponent<WeaponsGraphics>().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
    }

    public void PVETurretFireSound()
    {
        //SoundManager.Instance.PlayOneShot(SoundManager.Instance.gunFire);
        //second umber scales the audio volumne
        pveTurretGunSound.Play();
        
    }

    public void PVETurretStopFire()
    {
        CancelInvoke("PVETurretFireSound");
    }
}
