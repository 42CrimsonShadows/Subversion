using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image heatBarL;
    public Image heatbarR;

    public MachineGun machinegunScript;

    void Start()
    {
        machinegunScript = GetComponent<MachineGun>();
    }

    void Update()
    {
        heatBarL.fillAmount = machinegunScript.maxOverheatValue / 100f * machinegunScript.overheatValue1;
        heatbarR.fillAmount = machinegunScript.maxOverheatValue / 100f * machinegunScript.overheatValue2; 
    }

}
