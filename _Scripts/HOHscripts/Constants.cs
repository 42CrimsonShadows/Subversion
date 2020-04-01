using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

    //Scenes
    public const string SceneStartMenu = "HoH-StartMenu";
    public const string SceneQuarters = "HoH-Quarters";
    public const string SceneLoadingDocks = "HoH-LoadingDocks";
    public const string SceneLobbyScene = "lobbyScene";
    public const string SceneBattle1 = "HoHL-Mission1";

    //Maching Gun Types
    public const string MG1 = "MachineGun1";
    public const string MG2 = "MachineGun2";
    public const string MG3 = "MachineGun3";

    //Missile Pod Types
    public const string MS1 = "MissielPod1";
    public const string MS2 = "MissielPod2";
    public const string MS3 = "MissielPod3";

    //Engine Type
    public const string Eng1 = "Engine1";
    public const string Eng2 = "Engine2";
    public const string Eng3 = "Engine3";

    //Shield Type
    public const string SHD1 = "Shield1";
    public const string SHD2 = "Shield2";
    public const string SHD3 = "Shield3";

    //Leg Type
    public const string Leg1 = "Legs1";
    public const string Leg2 = "Legs2";
    public const string Leg3 = "Legs3";

    //E-Frame Type
    public const string LightFrame = "Light-E-Frame";
    public const string MedFrame = "Medium-E-Frame";
    public const string HeavyFrame = "Heavy-E-Frame";

    //E-Frame Faction Skin
    public const string xImperium = "X-Imperium";
    public const string fusionCoSyndicate = "Fusion-Co Syndicate";
    public const string shadahCollective = "Shadah Collective";
    public const string zeroG = "Zero-G";
    public const string renovoCore = "Renovo Core";
    public const string goldenSun = " Golden Sun";

    //Pickup Types
    public const int PickUpDataPack = 1;

    //Misc
    public const string Game = "Game";
    public const float CameraDefaultZoom = 60f;

    //keep track of all possible pickup types
    public static readonly int[] AllPickupTypes = new int[1]
    {
        //add more pickup types here if needed
        PickUpDataPack
    };

}
