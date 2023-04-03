using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    [SerializeField]
    Transform positionToTp;
    PlayerEntity player;
    public Action OnTpUse;
    

    static bool canBeUsed=true;

    private void Awake()
    {
        canBeUsed = true;
        ChangeTP_Function(Teleport);
    }


    public void ChangeTP_Function(Action L_method) =>  OnTpUse = L_method;

    void Teleport() => player.transform.position = positionToTp.position;


    private void OnTriggerEnter(Collider other)
    {
        var p = other.gameObject.GetComponent<PlayerEntity>();
        if (p!=null)
        {
           player = p;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (player != null && canBeUsed)
        {
            OnTpUse?.Invoke();
            StartCoroutine(TpCooldown());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var p = other.gameObject.GetComponent<PlayerEntity>();
        if (p != null)
        {
           player = null;
        }
    }

    IEnumerator TpCooldown()
    {
        canBeUsed=false;
        yield return new WaitForSeconds(1.5f);
        canBeUsed=true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,positionToTp.position);
        Gizmos.DrawWireSphere(positionToTp.position,2f);
    }







}
