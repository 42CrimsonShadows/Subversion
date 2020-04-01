using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMod : MonoBehaviour
{
    [SerializeField]
    private Vector3 newGravity;

    private void Awake()
    {
        Physics.gravity = newGravity;
    }
}
