using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : GunFather
{
    public override void Shoot()
    {
        if (canon!= null)
        {
            BulletManager.instance.SpawnBullet(canon, canon.rotation, bullet_damage, bullet_speed);
        }
       
        //sonido,particulas
      
    }
    // la pistola no usa habilidad especial
    public override void AddGunCharge(int chargeGain)
    {
        return;
    }
    public override void Ability()
    {
        return;
    }
    public override void SpecialAbilityTrigger()
    {
        return;
    }

    public override void OnEquip()
    {
        return;
    }

}
