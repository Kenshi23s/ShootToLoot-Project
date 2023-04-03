using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousState : IState
{
    Movement _movement;
    Bee _bee;
    StateMachine state;
    float patrolspeed, detectRadius;

    public SuspiciousState(Movement _movement,Bee _bee, float patrolspeed,float detectRadius, StateMachine state)
    {
        this._movement = _movement;
        this._bee = _bee;
        this.patrolspeed = patrolspeed;
        this.detectRadius = detectRadius;
        this.state = state;
    }
  
    public void OnEnter()
    {
        _movement._maxSpeed = patrolspeed;
    }

    public void OnUpdate()
    {      
        if (GoToLastPos()==true)
        {
            var player=_bee.CheckPlayerNearby(detectRadius);
            if (player!=null)
            {
                Debug.Log("Changing State To Hunt From Suspicious");
                state.ChangeState("Hunt");
            }
            else
            {
                Debug.Log("Changing State To Patrol From Suspicious");
                state.ChangeState("Patrol");
            }
        }      
    }
  
    bool GoToLastPos()
    {
        _movement.Seek(_bee.LastKnownPlayerPos);
        Vector3 dir = _bee.LastKnownPlayerPos - _movement._transform.position;
        if (detectRadius >= dir.magnitude)
        {
          return true;
        }       
        return false;
    }

    public void OnExit()
    {
        return;
    }

    public void ShowStateGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_movement._transform.position, _bee.LastKnownPlayerPos);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_bee.LastKnownPlayerPos, detectRadius);
    }    
}
