using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialGuns : GunFather
{
    [Header("SpecialGunVariables")]
    public int ammo, maxAmmo;
    public AudioClip special_clip;
    public override void AddGunCharge(int chargeGain)
    {
        {
            actualSpecialCharge += chargeGain;
            if (actualSpecialCharge < specialMaxCharge)
            {
                //actualiza la barra de carga especial con el nuevo porcentaje
            }
            else
            {
                //actualiza la barra y pone un icono de que esta cargada

            }

        }
    }
    public abstract void SpecialShoot();
    public abstract void SpecialAbility();

    private void LateUpdate()
    {
        actualSpecialCharge=Mathf.Clamp(actualSpecialCharge, 0, specialMaxCharge);
    }
    public override void SpecialAbilityTrigger()
    {
        if (actualSpecialCharge >= specialMaxCharge)
        {
            Ability();
            actualSpecialCharge = 0;
            print(actualSpecialCharge);
            //actualiza ui
        }
    }
    public override void OnEquip()
    {
        ammo=maxAmmo;  
    }
    public void NoAmmo()
    {
      var pistol = GunManager.chooseGun.Epistol;
      GunManager.instance.EquipGun(pistol);
    }

    public override void Shoot()
    {
        print("hola");
        SpecialShoot();
    }

    public override void Ability()
    {
        SpecialAbility();
    }
   
    protected virtual void SpecialFeedback()
    {
        AudioPool.instance.SpawnAudio(special_clip, canon.position);
    }

}
