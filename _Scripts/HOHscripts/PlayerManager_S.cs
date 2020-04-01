using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager_S : NetworkBehaviour {

    public static PlayerManager_S Instance = null;

    //SINGLETON
    //make sure there is only ever one copy of this gameobject in existence
    public void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        //persist througout the game
        DontDestroyOnLoad(Instance);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
