using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestructible : Entity
{

    public override void StartMethod()
    {
        return;
    }
    public override void OnTakeDamage(int dmg)
    {
        //TextPool.instance.PopUpDamageText(dmg,transform.position);
        //SpawnParticles(ParticleSample);
        life-=dmg;
        if (life <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        //AudioPool.instance.SpawnAudio(Aclip, transform.position);
        SpawnParticles(ParticleSample);
        Destroy(this.gameObject);

    }

    public override void OnDestroyCheck()
    {
        return;
    }

}
