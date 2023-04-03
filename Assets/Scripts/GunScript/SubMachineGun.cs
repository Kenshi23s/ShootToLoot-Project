using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FacundoColomboMethods;

public class SubMachineGun : SpecialGuns
{
    [Header("M4 Variables")]
    public float BuffedRateOfFire;
    float _baseRateFire;
    public float TimeBuffed;
    [Range(0,15)]
    public float DivideRateOfFire;

    public List<MeshRenderer> skins;

    private void Awake()
    {
        skins = ColomboMethods.GetChildrenComponentsList<MeshRenderer>(transform);

        if (PlayerPrefs.HasKey("M4Skin"))
        {
            Debug.Log("tengo la llave");
            switch (PlayerPrefs.GetString("M4Skin"))
            {
                case "Gold":
                    SwitchSkin(1);
                    return;
                default:
                    SwitchSkin(0);
                    return;
            }
        }
        else
        {

            PlayerPrefs.SetString("M4Skin", "Default");
        }






    }

    void SwitchSkin(int indexActivate)
    {
        skins[indexActivate].gameObject.SetActive(true);
        for (int i = 0; i < skins.Count; i++)
        {
            if (indexActivate==i)
                    continue;            
            skins[i].gameObject.SetActive(false);
        }
    }
    protected override void Start()
    {
        base.Start();
        _baseRateFire = rate_of_fire;
        BuffedRateOfFire = rate_of_fire / DivideRateOfFire;
        BuffedRateOfFire = Mathf.Clamp(BuffedRateOfFire, 0, rate_of_fire);
    }
    
  
    public override void SpecialShoot()
    {
        //terminar esto, llamar un metodo en 
        GunAnimator.SetBool("IsShooting", true);
        BulletManager.instance.SpawnBullet(canon, canon.rotation, bullet_damage, bullet_speed);
    }
    public override void SpecialAbility() => StartCoroutine(BuffTime(TimeBuffed));

    IEnumerator BuffTime(float t)
    {
        canGainCharge = false;
        rate_of_fire = BuffedRateOfFire;
        yield return new WaitForSeconds(t);
        rate_of_fire = _baseRateFire;
        canGainCharge = true;
    }

   
}
  

    

