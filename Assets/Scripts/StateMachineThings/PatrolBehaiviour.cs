using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaiviour : IState
{
    StateMachine _stateMachine;
    Movement _movement;
    Bee _bee;
    LayerMask _colisionLayer;

    Vector3 _ActualWayPoint;  

    float _detectRadius, _waypointRadius, _patrolSpeed;

    public PatrolBehaiviour(Movement movement,Vector3 ActualWayPoint,Bee bee, LayerMask _colisionLayer, float detectRadius,float waypointRadius,float patrolSpeed,StateMachine state)
    {
        this._movement=movement;
        this._ActualWayPoint = ActualWayPoint;
        this._bee=bee;
        this._colisionLayer = _colisionLayer;
        this._detectRadius=detectRadius;
        this._waypointRadius=waypointRadius;
        this._patrolSpeed=patrolSpeed;
        this._stateMachine = state;
        
        
    }
 
    Vector3 GetDir()
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);
        Vector3 dir = new Vector3(x, 0, z);
        RaycastHit WallHit;
        if (Physics.Raycast(_movement._transform.position, dir, out WallHit, dir.magnitude, _colisionLayer))
        {
           
            if (WallHit.transform.gameObject != null)
            {

                //GameManager.instance.WaitFrameEnd();
                //GameManager.instance.InstantiateCubeForTest(dir);
                return WallHit.point;
            }
            else
            {
                //GameManager.instance.InstantiateCubeForTest(dir);
                Debug.Log(WallHit.transform.position);
                return GetDir();
            }
        }
        else
        {
            Debug.Log(dir);
            return GetDir();
        }
       
    }
    public void OnEnter()
    {
       
        _movement._maxSpeed = _patrolSpeed;
        MakeDecision();
        
    }

    public void OnUpdate()
    {
     
        Move();
        MakeDecision();
    }

    void Move()
    {
        Vector3 actualForce = Vector3.zero;
        Vector3 dir = _ActualWayPoint - _movement._transform.position;

        if (_waypointRadius >= dir.magnitude)
        {
            _ActualWayPoint = GetDir();
        }

        actualForce += _movement.Seek(_ActualWayPoint);
        _movement.AddForce(actualForce);
    }

    void MakeDecision()
    {
        var player = _bee.CheckPlayerNearby(_detectRadius);
       
        if (player!=null)
        {
            Debug.Log("Changing State To Hunt From Patrol");
            _stateMachine.ChangeState("Hunt");
        }
    }

    public void OnExit()
    {
        return;
    }

    public void ShowStateGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_ActualWayPoint, _waypointRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_bee.transform.position, _detectRadius);

        //Gizmos.color = Color.yel;
        //Gizmos.DrawLine(_bee.transform.position, _ActualWayPoint);



    }

}
