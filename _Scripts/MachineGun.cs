using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class MachineGun : Gun
{

    protected override void Update()
    {
        base.Update();

        if (!isLocalPlayer)
        {
            return;
        }
        if ((Time.time - lastMachineGun1FireTime) > machineGunFireRate)
        {

            //MachineGun has auto fire on short cooldown
            //OVRController/////
            if (UnityEngine.XR.XRDevice.isPresent)
            {
                //if left OVR trigger
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0f && !gunShutdown1)
                {
                    if ((Time.time - lastMachineGun1FireTime) > machineGunFireRate)
                    {
                        overheatValue1 += gunOverheatSpeed * Time.deltaTime;
                        Debug.Log("Firing 1");
                        lastMachineGun1FireTime = Time.time;
                        FireMachineGun1();
                    }
                }


                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0f && !gunShutdown2)
                {
                    if ((Time.time - lastMachineGun2FireTime) > machineGunFireRate)
                    {
                        overheatValue2 += gunOverheatSpeed * Time.deltaTime;
                        Debug.Log("Firing 2");
                        lastMachineGun2FireTime = Time.time;
                        FireMachineGun2();
                    }
                }

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= 0f)
                {
                    overheatValue1 -= gunHeatRegenSpeed * Time.deltaTime;
                    StopFire1();
                }
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= 0f)
                {
                    overheatValue2 -= gunHeatRegenSpeed * Time.deltaTime;
                    StopFire2();
                }
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= 0f && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= 0f)
                {
                    overheatValue1 -= gunHeatRegenSpeed * Time.deltaTime;
                    overheatValue2 -= gunHeatRegenSpeed * Time.deltaTime;
                    //StopFireAll();
                    StopFire1();
                    StopFire2();

                }
                if (overheatValue1 >= maxOverheatValue)
                {
                    gunShutdown1 = true;
                    gunSoundsL[0].Play();
                    warningTextL.enabled = true;
                    heatTextL.enabled = false;
                }
                if (overheatValue2 >= maxOverheatValue)
                {
                    gunShutdown2 = true;
                    gunSoundsL[0].Play();
                    warningTextR.enabled = true;
                    heatTextR.enabled = false;
                }
                if (gunShutdown1 && overheatValue1 <= minOverheatValue)
                {
                    gunShutdown1 = false;
                    warningTextL.enabled = false;
                    heatTextL.enabled = true;
                    gunSoundsL[1].Play();
                }
                if (gunShutdown2 && overheatValue2 <= minOverheatValue)
                {
                    gunShutdown2 = false;
                    warningTextR.enabled = false;
                    heatTextR.enabled = true;
                    gunSoundsR[1].Play();
                }

                overheatValue1 = Mathf.Clamp(overheatValue1, minOverheatValue, maxOverheatValue);
                overheatValue2 = Mathf.Clamp(overheatValue2, minOverheatValue, maxOverheatValue);
            }
            else
            {
                //if mouse left click
                if (Input.GetMouseButton(0))
                {
                    if (!gunShutdown1 && !gunShutdown2)
                    {
                        if ((Time.time - lastMachineGun1FireTime) > machineGunFireRate && (Time.time - lastMachineGun2FireTime) > machineGunFireRate)
                        {
                            overheatValue1 += gunOverheatSpeed * Time.deltaTime;
                            overheatValue2 += gunOverheatSpeed * Time.deltaTime;

                            FireMachineGun1();
                            FireMachineGun2();

                            lastMachineGun1FireTime = Time.time;
                            lastMachineGun2FireTime = Time.time;
                        }
                    }
                }


                if (overheatValue1 >= maxOverheatValue)
                {
                    gunShutdown1 = true;
                    gunSoundsL[0].Play();
                    warningTextL.enabled = true;
                    heatTextL.enabled = false;
                }
                if (overheatValue2 >= maxOverheatValue)
                {
                    gunShutdown2 = true;
                    gunSoundsL[0].Play();
                    warningTextR.enabled = true;
                    heatTextR.enabled = false;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    StopFire1();
                    StopFire2();
                    //StopFireAll();
                }

                if (overheatValue1 >= minOverheatValue)
                {
                    overheatValue1 -= gunHeatRegenSpeed * Time.deltaTime;

                }

                if (overheatValue2 >= minOverheatValue)
                {
                    overheatValue2 -= gunHeatRegenSpeed * Time.deltaTime;
                }
                if (gunShutdown1 && overheatValue1 <= minOverheatValue)
                {
                    gunShutdown1 = false;
                    warningTextL.enabled = false;
                    heatTextL.enabled = true;
                    gunSoundsL[1].Play();
                }
                if (gunShutdown2 && overheatValue2 <= minOverheatValue)
                {
                    gunShutdown2 = false;
                    warningTextR.enabled = false;
                    heatTextR.enabled = true;
                    gunSoundsR[1].Play();
                }

                //keep the heat level of the mechineguns between 0 and 1000
                overheatValue1 = Mathf.Clamp(overheatValue1, minOverheatValue, maxOverheatValue);
                overheatValue2 = Mathf.Clamp(overheatValue2, minOverheatValue, maxOverheatValue);
            }
        }
    }
}
