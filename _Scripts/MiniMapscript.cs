using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapscript : MonoBehaviour
{
    public Transform minMapPlayer;



    void LateUpdate()
    {
        if (minMapPlayer == null)
        {
            Debug.Log("MiniMapScript: No miniMapPlayer Gameobject Found.");
            Debug.Log("MiniMapScript: Attempting to Locate.");
            minMapPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //updating camera position to the player position
        Vector3 newPosition = minMapPlayer.position;
        newPosition.y = minMapPlayer.position.y;
        transform.position = newPosition;

        //updating the camer rotation to the player rotation
        transform.rotation = Quaternion.Euler(90f, minMapPlayer.eulerAngles.y, 0f);
    }
  
}
