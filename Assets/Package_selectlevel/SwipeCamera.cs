using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCamera : MonoBehaviour
{
    public float minLimitMap;
    public float maxLimitMap;
    public float speedSwipe;

    [Range(0,10)]
    public float speedCam;
    
    Vector3 initialTouch;
    Vector3 finalTouch;
    Vector3 distance;

    Vector3 actualPos;
    public Camera posCam;
    public LayerMask LayerLevels;


    private void Start()
    {
        actualPos = posCam.transform.position;
    }

    private void Update()
    {
        Vector3 result = distance + actualPos;

        result.z = Mathf.Clamp(result.z, minLimitMap, maxLimitMap);
        
        posCam.transform.position = Vector3.Lerp(posCam.transform.position,result,speedCam*Time.deltaTime);
    }

    public void BeginDrag()
    {
        initialTouch = (-Input.mousePosition) * speedSwipe; 
    }

    public void Drag()
    {
        finalTouch = (-Input.mousePosition) * speedSwipe;
        distance = finalTouch - initialTouch;
        distance = transformVector(distance.y);
    }

    public void EndDrag()
    {   
        actualPos = distance + actualPos;
        actualPos.z = Mathf.Clamp(actualPos.z, minLimitMap, maxLimitMap);

        distance = Vector3.zero;
        initialTouch = Vector3.zero;
        finalTouch = Vector3.zero;
    }

    public void TouchIcon()
    {
        RaycastHit hit;
        Ray ray = posCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, LayerLevels))
        {
            var OutHit = hit.transform.gameObject.GetComponent<Level>();

            if (OutHit)
            {
                if (!OutHit.MainLevelData.Locked)
                {

                        Debug.Log("acceso al nivel = " + OutHit);
                        LevelsManager.instance.GoToLevel(OutHit.MainLevelData);      

                }
                else
                {
                    Debug.Log("nivel bloqueado = " + OutHit);
                }
            }
            else
            {
                Debug.LogWarning("no hay referencia del nivel !!");
            }
            
        }
    }

    private Vector3 transformVector(float z)
    {
        Vector3 vector3 = new Vector3(0, 0, z);
        return vector3;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(50 , 0,minLimitMap), 5);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(50, 0, maxLimitMap), 5);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(50, 0, minLimitMap), new Vector3(50,0,maxLimitMap));
    }
}
