using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : SpecialGuns
{
    //public Rigidbody rb;
    [Header("ShotgunVariables")]
    public float dispersion=5f;
    public float bulletsToShoot=5;
    public int dist = 15;

    
    public override void SpecialAbility()
    {
      
    }

    public override void SpecialShoot()
    {       
        float maxAngle = (dist * bulletsToShoot)*0.5f;
        for (int i = 0; i < bulletsToShoot; i++)
        {
            float myspread= ShotgunSpread(i,dist,maxAngle);
            BulletManager.instance.SpawnBullet(canon, canon.rotation*Quaternion.Euler(1,myspread,1), bullet_damage, bullet_speed);
           
        }
       //_Asource.PlayOneShot(shooting_clip);       
    }

   float ShotgunSpread(int I,int distancePerBullet,float maxAngle) => I * distancePerBullet - maxAngle; 

   //Vector3 RandomizePatern()
   // {
   //     Vector3 Actualdir = canon.transform.position - canon.forward;
   //     Actualdir = new Vector3
   //         (Actualdir.x ,
   //         Actualdir.y,
   //         Actualdir.z + Random.Range(-dispersion, dispersion)
   //         );
   //     Vector3 Newdir = Actualdir - canon.transform.position;
   //     return Newdir.normalized;
   // }

}
