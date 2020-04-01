using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedHandRotation : MonoBehaviour
{
    Quaternion rotation;

    void Awake()
    {
        //if (rotation == null)
            rotation = transform.rotation;

        foreach (Transform child in transform)
        {
            if (child.tag == "CustomHand")
                child.gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
        //this.gameObject.transform.GetChild(0).gameObject.transform.rotation = rotation;
    }
}
