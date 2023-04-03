using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFather : MonoBehaviour
{
    [Header("GunVariables")]
   public bool canGainCharge, canShoot = true;
   public int bullet_damage,bullet_speed, actualSpecialCharge, specialMaxCharge;
   [SerializeField]
    [Range(1,0)]
   public float rate_of_fire;
   // el transform cannon se debe usar para definir de donde saldra la bala
   public Transform canon;
   public GameObject ShootParticle_Sample;
   public AudioClip shooting_clip, toEquip_clip;
   public Animator GunAnimator;
    
    public abstract void Shoot();
    public abstract void AddGunCharge(int chargeGain);
    public abstract void SpecialAbilityTrigger();
    public abstract void OnEquip();
    public abstract void Ability();
 

    protected virtual void Start()
    {
        var anim = this.gameObject.GetComponent<Animator>();
        if (anim!=null)
        {
            GunAnimator = anim;
        }
    }
    //se debe usar al agarrar el arma(Especiales) o al volverla a equipar(pistola)
    public virtual void ToEquip()
    {
        AudioPool.instance.SpawnAudio(toEquip_clip, canon.position);
    }

   protected virtual void ShootFeedback()
   {
       if (ShootParticle_Sample!=null)
       {
            Instantiate(ShootParticle_Sample, canon.position, canon.rotation);
       }
       AudioPool.instance.SpawnAudio(shooting_clip, canon.position);
    }
   public void OnTriggerPull()
   {
        if (canShoot==true)
        {
            Shoot();
            ShootFeedback();
            StartCoroutine(ShootCooldown());
        }
    
   }
   public IEnumerator ShootCooldown()
   {
        canShoot = false;
        yield return new WaitForSeconds(rate_of_fire);
        canShoot = true;
   } 

}
