using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIdentificator : MonoBehaviour
{
    public Transform FollowObject;
    public float RotationSpeed;

 

    // Update is called once per frame
    void Update()
    {
        FollowTransform();

        Rotate(RotationSpeed);
    }

    void FollowTransform()
    {
        if (FollowObject)
        {
            transform.position = FollowObject.position + new Vector3(0,0.1f,0);
        }
       
    }
    void Rotate(float RotationSpeed)
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0,0,RotationSpeed * Time.deltaTime);
    }
}
