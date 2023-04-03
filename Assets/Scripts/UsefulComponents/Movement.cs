using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement 
{

    Vector3 _velocity;
    internal Transform _transform;
    public float _maxSpeed;

    public float _maxForce;

    public Movement(Vector3 velocity,Transform transform, float maxSpeed, float maxForce)
    {
        _velocity = velocity;
       _transform = transform;
        _maxSpeed = maxSpeed;
        _maxForce = maxForce;

    }
       
    /// <summary>
    /// Este metodo se usa para Evadir x objetivo en base a una posicion que tendra(el objetivo) en el futuro.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    /// 
    public Vector3 ArriveToTarget(Entity target)
    {
        Vector3 desired = target.transform.position - _transform.position;
        float dist = desired.magnitude;
        desired.Normalize();
        //if (dist <= GameManager.instance.ArriveRadius)
        //{
        //    desired *= _maxSpeed * (dist / GameManager.instance.ArriveRadius);
        //}
        //else
        //{
            desired *= _maxSpeed;
        //}

        //Steering
        Vector3 steering = desired - _velocity;

        return steering;
    }

    public Vector3 Seek(Vector3 targetSeek)
    {
        Vector3 desired = targetSeek - _transform.position;
        desired.Normalize();
        desired *= _maxForce;

        //Vector3 steering = desired - _velocity;

        //steering = Vector3.ClampMagnitude(steering, _maxSpeed);

        return desired;
    }
    /// <summary>
    /// Este metodo se usa para Evadir x objetivo en base a una posicion que tendra(el objetivo) en el futuro.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    //public Vector3 EvadeTarget(Entity target)
    //{

    //    Vector3 finalPos = (target.transform.position + target._velocity) * Time.deltaTime;
    //    Vector3 desired  = _transform.position - finalPos;
    //    return SteerTo(desired);

    //}
    //girar hacia
    Vector3 SteerTo(Vector3 desiredPos)
    {
        desiredPos.Normalize();
        desiredPos *= _maxSpeed;
        Vector3 steering = desiredPos - _velocity;
        return steering;
    }

    public void AddForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        _transform.position += _velocity * Time.deltaTime;
        _transform.forward = _velocity;
    }
    //public Vector3 Pursuit(Entity target)
    //{
    //    Vector3 var = target.transform.position + (target._velocity * Time.deltaTime);
    //    Debug.Log(var);
    //    Vector3 finalPos = var;   
    //    Debug.Log(finalPos);
    //    Vector3 desired = finalPos - _transform.position;
    //    return SteerTo(desired);
    //}
    public void MovementGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_transform.position, _transform.position + _velocity);

    }
}
