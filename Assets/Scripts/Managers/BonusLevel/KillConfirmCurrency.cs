using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillConfirmCurrency : SuckTowardsTrigger
{
    int value=1;
    public override void EffectTrigger(Collider other)=> ModesManager.instance.gameMode.points += value;





}
