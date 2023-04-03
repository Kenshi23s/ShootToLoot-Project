using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuckTowardsNearby : SuckTowardsPlayer
{
    public float InteractRange;
    public override void LateUpdate()
    {
        base.LateUpdate();

        //InteractRange = Mathf.Clamp(InteractRange, 0, PlayerDetect);
        if (InteractRange > dir.magnitude)
        {
            Interact();
        }
    }
    public abstract void Interact();

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color=Color.gray;
        Gizmos.DrawWireSphere(transform.position, InteractRange);

    }
}
