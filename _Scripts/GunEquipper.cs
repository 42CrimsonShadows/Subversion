using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// THIS SCRIPT WOULD NORMALLY BE ATTACHED TO THE PLAYER GAMEOBJECT
/// </summary>

public class GunEquipper : NetworkBehaviour {

    //active compents accessable to other scripts
    public static string activeMachineGunType;
    public static string activeMissilePodType;
    public static string activeEngineType;
    public static string activeShieldType;
    public static string activeLegType;
    public static string activeFrameType;
    public static string activeFactionType;

    //gun types
    public GameObject MG1;
    public GameObject MG2;
    public GameObject MG3;
    //missile pod types
    public GameObject MS1;
    public GameObject MS2;
    public GameObject MS3;
    //engine types
    private GameObject Eng1;
    private GameObject Eng2;
    private GameObject Eng3;
    //shield types
    public GameObject SHD1;
    public GameObject SHD2;
    public GameObject SHD3;
    //leg types
    public GameObject Leg1;
    public GameObject Leg2;
    public GameObject Leg3;
    //frame types
    public GameObject LightFrame;
    public GameObject MedFrame;
    public GameObject HeavyFrame;
    //faction
    public GameObject xImperium;
    public GameObject fusionCoSyndicate;
    public GameObject shadahCollective;
    public GameObject zeroG;
    public GameObject renovoCore;
    public GameObject goldenSun;

    //active components
    GameObject activeMachineGun;  
    GameObject activeMissilePod;  
    GameObject activeEngine;
    GameObject activeShield;        
    GameObject activeLeg;    
    GameObject activeFrame;
    GameObject activeFaction;

    //prefrences selected bool values
    public bool mg1status;
    public bool mg2status;
    public bool mg3status;
    public bool ms1status;
    public bool ms2status;
    public bool ms3status;
    public bool sh1status;
    public bool sh2status;
    public bool sh3status;
    public bool leg1status;
    public bool leg2status;
    public bool leg3status;
    public bool eng1status;
    public bool eng2status;
    public bool eng3status;
    public bool mLightstatus;
    public bool mMediumdstatus;
    public bool mHeavystatus;
    public bool fSel1status;
    public bool fSel2status;
    public bool fSel3status;
    public bool fSel4status;
    public bool fSel5status;
    public bool fSel6status;

    //damage, range, and gun attributes
    public float gunDamage;
    public float gunRange;

    public float missileDamage;
    public float missileRange;

    private void Start()
    {
        //The equipment you currently have
        //activeMachineGunType = Constants.MG1;
        //activeMissilePodType = Constants.MS1;
        //activeShieldType = Constants.SHD1;
        //activeEngineType = Constants.Eng1;
        //activeLegType = Constants.Leg1;
        //activeFrameType = Constants.LightFrame;
        //activeFactionType = Constants.xImperium;

        ////initializing the starting eqipment, player frame and faction
        //activeMachineGun = MG1;
        //activeMissilePod = MS1;
        //activeEngine = Eng1;
        //activeShield = SHD1;
        //activeLeg = Leg1;
        //activeFrame = LightFrame;
        //activeFaction = xImperium;

        //setting current Boolean values
        mg1status = true;
        ms1status = true;
        eng1status = true;
        sh1status = true;
        leg1status = true;
        mLightstatus = true;
        fSel1status = true;

    }

    private void LoadMachineGun(GameObject machineGun)
    {
        //turn off all machinegun objects
        MS1.SetActive(false);
        MG2.SetActive(false);
        MG3.SetActive(false);

        //set the passed in gameobject as active and update the activegun reference
        machineGun.SetActive(true);
        activeMachineGun = machineGun;
    }
    private void LoadMissilePod(GameObject missilePod)
    {
        MS1.SetActive(false);
        MS2.SetActive(false);
        MS3.SetActive(false);

        missilePod.SetActive(true);
        activeMissilePod = missilePod;
    }
    //private void LoadEngine(GameObject engine)
    //{
    //    Eng1.SetActive(false);
    //    Eng2.SetActive(false);
    //    Eng3.SetActive(false);

    //    engine.SetActive(true);
    //    activeEngine = engine;
    //}
    private void LoadShield(GameObject shield)
    {
        SHD1.SetActive(false);
        SHD2.SetActive(false);
        SHD3.SetActive(false);

        shield.SetActive(true);
        activeEngine = shield;
    }
    private void LoadLeg(GameObject leg)
    {
        Leg1.SetActive(false);
        Leg2.SetActive(false);
        Leg3.SetActive(false);

        leg.SetActive(true);
        activeEngine = leg;
    }
    private void LoadFrame(GameObject eFrame)
    {
        LightFrame.SetActive(false);
        MedFrame.SetActive(false);
        HeavyFrame.SetActive(false);

        eFrame.SetActive(true);
        activeEngine = eFrame;
    }
    private void LoadFaction(GameObject faction)
    {
        xImperium.SetActive(false);
        fusionCoSyndicate.SetActive(false);
        shadahCollective.SetActive(false);
        zeroG.SetActive(false);
        renovoCore.SetActive(false);
        goldenSun.SetActive(false);

        faction.SetActive(true);
        activeEngine = faction;
    }

    void Update()
    {
        //if there is a player mech in the scene...
        if (GameObject.FindWithTag("Player") == true)
        {
            var playerEFrame = GameObject.FindWithTag("Player");

            if(MG1 == null && MG2 == null && MG3 == null)
            {
                Debug.Log("Loading Eqipment References");

                MG1 = GameObject.Find("MG11");
                MG2 = GameObject.Find("MG22");
                MG3 = GameObject.Find("MG33");

                MS1 = GameObject.Find("MS1");
                MS2 = GameObject.Find("MS2");
                MS3 = GameObject.Find("MS3");

                SHD1 = GameObject.Find("SD1");
                SHD2 = GameObject.Find("SD2");
                SHD3 = GameObject.Find("SD3");

                Leg1 = GameObject.Find("Leg1");
                Leg2 = GameObject.Find("Leg2");
                Leg3 = GameObject.Find("Leg3");

                LightFrame = GameObject.Find("E_Frame_Light");
                MedFrame = GameObject.Find("E_Frame_Medium");
                HeavyFrame = GameObject.Find("E_Frame_Heavy");

                xImperium = GameObject.Find("FactionSel1");
                fusionCoSyndicate = GameObject.Find("FactionSel2");
                shadahCollective = GameObject.Find("FactionSel3");
                zeroG = GameObject.Find("FactionSel4");
                renovoCore = GameObject.Find("FactionSel5");
                goldenSun = GameObject.Find("FactionSel6");

                EQrefresh();
            }
            else
            {
                return;
            }
        }
    }

    public void EQrefresh()
    {
            //MachineGun Selection menu
            if (mg1status)
            {
                LoadMachineGun(MG1);
                activeMachineGunType = Constants.MG1;
            }
            if (mg2status)
            {
                LoadMachineGun(MG2);
                activeMachineGunType = Constants.MG2;
            }
            if (mg3status)
            {
                LoadMachineGun(MG3);
                activeMachineGunType = Constants.MG3;
            }

            //Missile Selction menu
            if (ms1status)
            {
                LoadMissilePod(MS1);
                activeMissilePodType = Constants.MS1;
            }
            else if (ms2status)
            {
                LoadMissilePod(MS2);
                activeMissilePodType = Constants.MS2;
            }
            else if (ms3status)
            {
                LoadMissilePod(MS3);
                activeMissilePodType = Constants.MS3;
            }

            //Engine Selection menu
            //if (eng1status)
            //{
            //    LoadEngine(Eng1);
            //    activeEngineType = Constants.Eng1;
            //}
            //else if (eng2status)
            //{
            //    LoadEngine(Eng2);
            //    activeEngineType = Constants.Eng2;
            //}
            //else if (eng3status)
            //{
            //    LoadEngine(Eng3);
            //    activeEngineType = Constants.Eng3;
            //}

            //Shield Selection menu
            if (sh1status)
            {
                LoadShield(SHD1);
                activeShieldType = Constants.SHD1;
            }
            else if (sh2status)
            {
                LoadShield(SHD2);
                activeShieldType = Constants.SHD2;
            }
            else if (sh3status)
            {
                LoadShield(SHD3);
                activeShieldType = Constants.SHD3;
            }

            //leg Selction menu
            if (leg1status)
            {
                LoadLeg(Leg1);
                activeLegType = Constants.Leg1;
            }
            else if (leg2status)
            {
                LoadLeg(Leg2);
                activeLegType = Constants.Leg2;
            }
            else if (leg3status)
            {
                LoadLeg(Leg3);
                activeLegType = Constants.Leg3;
            }

            //Frame Selction Menu
            if (mLightstatus)
            {
                LoadFrame(LightFrame);
                activeFrameType = Constants.LightFrame;
            }
            else if (mMediumdstatus)
            {
                LoadFrame(MedFrame);
                activeFrameType = Constants.MedFrame;
            }
            else if (mHeavystatus)
            {
                LoadFrame(HeavyFrame);
                activeFrameType = Constants.HeavyFrame;
            }

            //faction selection menu
            if (fSel1status)
            {
                LoadFaction(xImperium);
                activeFactionType = Constants.xImperium;
            }
            else if (fSel2status)
            {
                LoadFaction(fusionCoSyndicate);
                activeFactionType = Constants.fusionCoSyndicate;
            }
            else if (fSel3status)
            {
                LoadFaction(shadahCollective);
                activeFactionType = Constants.shadahCollective;
            }
            else if (fSel4status)
            {
                LoadFaction(zeroG);
                activeFactionType = Constants.zeroG;
            }
            else if (fSel5status)
            {
                LoadFaction(renovoCore);
                activeFactionType = Constants.renovoCore;
            }
            else if (fSel6status)
            {
                LoadFaction(goldenSun);
                activeFactionType = Constants.goldenSun;
            }
    }

    public GameObject CheckForPlayerMech()
    {
        GameObject foundMech = GameObject.FindGameObjectWithTag("Player");

        if (foundMech.layer == 11)
        {
            Debug.Log("Found a Player Mech!");
        }

        return gameObject;
    }

    //RETURN ACTIVE ELEMENTS so other scripts can check them
    public GameObject GetActiveMachineGun()
    {
        return activeMachineGun;
    }
    public GameObject GetActiveMissilePod()
    {
        return activeMissilePod;
    }
    public GameObject GetActiveShield()
    {
        return activeShield;
    }
    public GameObject GetActiveEngine()
    {
        return activeEngine;
    }
    public GameObject GetActiveLeg()
    {
        return activeLeg;
    }
    public GameObject GetActiveFrame()
    {
        return activeFrame;
    }
    public GameObject GetActiveFaction()
    {
        return activeFaction;
    }
}