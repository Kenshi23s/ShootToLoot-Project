using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HuntState : IState
{
    StateMachine _state;
    float _interactRadius, _detectRadius ,_huntSpeed;
    int _AttackDamage;
    Bee _bee;
    Movement _movement;

    public HuntState(Movement movement, float interactRadius, float detectRadius, float huntSpeed, int AttackDamage, Bee bee, StateMachine state)
    {
        this._movement = movement;
        this._interactRadius = interactRadius;
        this._detectRadius = detectRadius;
        this._huntSpeed = huntSpeed;
        this._AttackDamage = AttackDamage;
        this._bee = bee;   
        this._state = state;
    }

    public void OnEnter()
    {
        var player = _bee.CheckPlayerNearby(_detectRadius);

        if (player == null)
        {
            _state.ChangeState("Suspicious");
            Debug.Log("Changing State To Suspicious From Hunt");
            return;
        }
        _movement._maxSpeed = _huntSpeed;  
    }
    public void OnUpdate()
    {
        Vector3 actualForce = Vector3.zero;
        var player = _bee.CheckPlayerNearby(_detectRadius);
        if (player != null)
        {
            Vector3 dir = player.transform.position - _movement._transform.position;

            actualForce +=_movement.Seek(GameManager.instance.PlayerPos.position);

            if (_interactRadius >= dir.magnitude)
            {
                _bee.OnAttack(_AttackDamage,player);

            }
        }

        else
        {
            Debug.Log("Changing State To Suspicious From Hunt");
            _state.ChangeState("Suspicious");
        }
        _movement.AddForce(actualForce);
    }
   
    public void OnExit()
    {
        return;
    }

    public void ShowStateGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawLine(_bee.transform.position, GameManager.instance.PlayerPos.position);

       Gizmos.color = Color.blue;
       Gizmos.DrawWireSphere(_bee.transform.position, _detectRadius);

       Gizmos.color = Color.magenta;
       Gizmos.DrawWireSphere(_bee.transform.position, _interactRadius);
    }

   
}
