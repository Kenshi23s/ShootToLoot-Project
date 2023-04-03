using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNav : EnemyEntity
{
    
    NavMeshAgent thisAgent;
    Action<ZombieNav> ReturnMethod;
    public float HitRange,PushForce;
    HardPoint hp;
    
    

    public void InitializeZombie(Action<ZombieNav> ReturnMethod) => this.ReturnMethod = ReturnMethod;

    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        thisAgent.SetDestination(ZombieManager.Instance.playerPos);
        float distance = (ZombieManager.Instance.playerPos - transform.position).magnitude;
        if (distance <= HitRange)
        {

        }
    }

    public void BackToPool() => ReturnMethod(this);

    public override void EnemyOnTakeDamage(int dmg)
    {
        
    }

    public override void OnAttack(int dmg, PlayerEntity Entity)
    {
        throw new System.NotImplementedException();
    }
 

    public override void OnDestroyCheck()
    {
        
    }
    public override void Die()
    {
        
        if (hp != null)
        {
            hp.OnTriggerExit(gameObject.GetComponent<Collider>());
        }
        ReturnMethod(this);
        
    }
    public void SetHardpoint(HardPoint hp)=> this.hp = hp;

    
    public override void StartMethod()
    {
        
    }
}
