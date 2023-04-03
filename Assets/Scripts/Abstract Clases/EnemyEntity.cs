using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
    public Vector3 velocity;
    public GameObject DieParticle;
    public int _damage;
    Action<EnemyEntity> _ReturnMethod;
    Dictionary<int, EnemyEntity> _EnemyEntities;


    public void InitializeEnemy(Action<EnemyEntity> _ReturnMethod) 
    {
        //this._ReturnMethod = _ReturnMethod;
        StartMethod();
    }

 

    public abstract void EnemyOnTakeDamage(int dmg);
    
    public override void OnTakeDamage(int dmg)
    {
        life -= dmg;
        TextPool.instance.PopUpDamageText(dmg, transform.position);
        EnemyOnTakeDamage(dmg);
    }

    public void Attack(PlayerEntity playerEntity)
    {
        OnAttack(_damage, playerEntity);
    }
    public abstract void OnAttack(int dmg,PlayerEntity Entity);

}
