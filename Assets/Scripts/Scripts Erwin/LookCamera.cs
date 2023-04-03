using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookRotation(this.transform,Camera.main.transform.position);
    }

    void LookRotation(Transform Pos1, Vector3 Pos2) 
    {
        Pos2 = new Vector3(Pos1.position.x, Pos2.y, Pos2.z);

        Pos1.LookAt(Pos2);
        
    }
}
