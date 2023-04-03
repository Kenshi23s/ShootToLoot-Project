using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Focus : MonoBehaviour
{
    public Transform Focus;
    public float Velocity;  
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Focus.position, Velocity);
    }
}
