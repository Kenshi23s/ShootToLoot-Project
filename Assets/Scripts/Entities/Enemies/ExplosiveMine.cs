using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : SuckTowardsNearby,IDamagable
{
    [SerializeField]
    int _atkDamage = 15;
    [SerializeField]
    float ExplosiveRange = 15f;
    public override void Interact()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, ExplosiveRange);
        foreach (Collider col in collider)
        {
            var Idamageable = col.GetComponent<IDamagable>();
            if (Idamageable!=null)
            {
                Idamageable.TakeDamage(_atkDamage);
            }
        }
        Destroy(this.gameObject);
    }
    
    public override void OnDrawGizmos()
    {

        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);
    }

    void IDamagable.TakeDamage(int dmg)
    {
        
        TextPool.instance.PopUpDamageText(dmg, transform.position);
        Interact();

    }
}
