using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Gun {

    protected override void Update()
    {
        base.Update();

        if (!isLocalPlayer)
        {
            return;
        }

        //Missile has semi fire on long cooldown
        if(Time.time - lastMissile1FireTime > missileFireRate)
        {
            if (!missile1Ready)
            {
                missile1Ready = true;
                missileSoundsL[0].Play();
            }    
        }

        if (Time.time - lastMissile2FireTime > missileFireRate)
        {
            if (!missile2Ready)
            {
                missile2Ready = true;
                missileSoundsR[0].Play();
            }
        }

        //if Middle Mouse button pressed (USING THIS FOR THE LEFT GUN)
        if (Input.GetMouseButtonDown(2) || OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0f)
        {
            if(missile1Ready)
            {
                Debug.Log("MISSILE MISSILE MISSLE FIRE FIRE FIRE!!!!");

                CmdFireMissile1();
                missile1Ready = false;
                lastMissile1FireTime = Time.time;

            }
            else
            {
                //play not ready sound
                missileSoundsL[1].Play();
            }
        }

        //if Right Mouse button pressed (USING THIS FOR THE RIGHT GUN)
        if (Input.GetMouseButtonDown(1) || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0f)
        {
            if (missile2Ready)
            {
                Debug.Log("MISSILE MISSILE MISSLE FIRE FIRE FIRE!!!!");

                CmdFireMissile2();
                missile2Ready = false;
                lastMissile2FireTime = Time.time;
            }
            else
            {
                //play not ready sound
                missileSoundsR[1].Play();
            }
        }

        ///OVRController

        ////if left hand trigger
        //if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0f || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0f)
        //{
        //    if (Time.time - lastMissileFireTime > missileFireRate)
        //    {
        //        lastMissileFireTime = Time.time;
        //        FireMissile();
        //    }
        //}
        //else
        //{
        //    StopFire();
        //}

        ////if right hand trigger
        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0f)
        //{
        //    if (Time.time - lastMissileFireTime > missileFireRate)
        //    {
        //        lastMissileFireTime = Time.time;
        //        FireMissile();
        //    }
        //}
    }
}
