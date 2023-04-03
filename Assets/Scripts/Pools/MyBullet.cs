using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : MonoBehaviour
{
    
    private int _damage;
    private float _speed;
    private Rigidbody _rb;
    Action<MyBullet> ReturnMethod;
    /// <summary>
    /// el arma que dispare la bala debe llamar a este metodo para que tenga valores diferentes de 0
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="speed"></param>
 
    public void Configure(Action<MyBullet> ReturnMethod)
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        this.ReturnMethod = ReturnMethod;
    }
    public void AssignValues(Transform pos ,Quaternion dir,int dmg,float speed)
    {
        transform.position = pos.position;
        transform.rotation = dir.normalized;
        _damage = dmg;
        _speed = speed;
    }
    public void ProjectileLaunch()
    {

        _rb.velocity = Vector3.zero;        
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        var Damagable = other.gameObject.GetComponent<IDamagable>();

        if (Damagable!=null)
        {
            Damagable.TakeDamage(_damage);
           
            
        }
        ReturnMethod(this);
    } 
    // mas adelante agregar un contador para que se destruya la bala desp de x tiempo O que vuelva a una pool de balas
}
