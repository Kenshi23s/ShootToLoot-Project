using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuckTowardsTrigger : SuckTowardsPlayer
{
    private void OnTriggerEnter(Collider other)
    {
        EffectTrigger(other);
    }
    public abstract void EffectTrigger(Collider other);

   
}
