using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyEntity
{
    public float detectionRange, interactRange, WaypointRange, maxSpeed, maxForce, patrolSpeed, huntSpeed;
    Animator _anim;
    [SerializeField]
    LayerMask _layerMask;

    #region StateMachineThings
    StateMachine stateMachine;
    Movement BeeMovement;
    SuspiciousState MySuspiciousState;
    PatrolBehaiviour MyPatrolState;
    HuntState MyhuntState;
    #endregion

    internal Vector3 LastKnownPlayerPos;
    Vector3 ActualWaypoint=Vector3.zero;
    Vector3 dir;

    public override void StartMethod()
    {
        EnemyManager.instance.AddToEnemyList(this);
        CreateStates();
    }

    void CreateStates() 
    {
        stateMachine = new StateMachine();
 

        BeeMovement = new Movement(velocity,this.transform,maxSpeed,maxForce);

        MySuspiciousState= new SuspiciousState(BeeMovement, this, patrolSpeed, detectionRange, stateMachine);

        MyPatrolState = new PatrolBehaiviour(BeeMovement, ActualWaypoint, this, _layerMask, detectionRange, WaypointRange, patrolSpeed, stateMachine);

        MyhuntState = new HuntState(BeeMovement, interactRange, detectionRange, huntSpeed, _damage, this, stateMachine);

        stateMachine.CreateState("Suspicious", MySuspiciousState);
        stateMachine.CreateState("Patrol", MyPatrolState);
        stateMachine.CreateState("Hunt", MyhuntState);

        stateMachine.ChangeState("Patrol");
    }

    public void ChangeLastPlayerPos(Vector3 pos)
    {
        LastKnownPlayerPos=pos;
        return;
    }
  
    private void Update()
    {
        //transform.eulerAngles = new Vector3(0, 0, 0);
        stateMachine.Execute();
    }
    public PlayerEntity CheckPlayerNearby(float PassedDetectRadius)
    {
        dir = GameManager.instance.PlayerPos.position - transform.position;

        if (PassedDetectRadius > dir.magnitude)
        {           
            RaycastHit hit;
           
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.transform.gameObject.name);
                var p = hit.transform.gameObject.GetComponent<PlayerEntity>();
                
                if (p != null)
                {
                    ChangeLastPlayerPos(hit.point);
                    return p;
                }
            }

        }
        return null;
      
    }

    public override void EnemyOnTakeDamage(int dmg)
    {
        life-=dmg;
        if (life>=0)
        {
            Die();
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void OnAttack(int dmg, PlayerEntity Entity)
    {
        Entity.TakeDamage(dmg);
        Destroy(this.gameObject);
    }

    public override void OnDestroyCheck()
    {
        return;
    }

    private void OnDrawGizmos()
    {
        BeeMovement.MovementGizmos();
        stateMachine.ShowGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + dir);
    }


}
