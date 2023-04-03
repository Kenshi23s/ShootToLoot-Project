using FacundoColomboMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillConfirmManager : GameModeBaseClass
{
    public override void InitializeMode()
    {
        pointsToWin = transform.childCount;
        foreach (GameObject item in ColomboMethods.GetChildrenComponents<GameObject>(transform))
        {
            item.gameObject.SetActive(true);
        }
    }
}
