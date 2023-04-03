using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement 
{
   
    Rigidbody rb;
    public float maxVelocity = 10, speed = 5, InfluenceLimit;
    [Range(0, 1)]
    public float slip = 0.3f;
    //este valor no puede estar por encima de slip
    public float BrakeLimit = 0.1f;
    Transform player;

    public PlayerMovement(Transform _player,Rigidbody _rb,float _speed,float _maxVelocity ,float _slip,float _BrakeLimit,float _InfluenceLimit)
    {
        player = _player;   
        rb = _rb;
        speed = _speed;      
        maxVelocity=_maxVelocity;
        slip = _slip;
        BrakeLimit = _BrakeLimit;
        InfluenceLimit = _InfluenceLimit;

    }

    public void Move(Vector3 MovementValue)
    {
       
        if (MovementValue != Vector3.zero)
        {
            if (MovementValue.magnitude>1)
            {
                MovementValue.Normalize();
            }
          
            rb.velocity += new Vector3(MovementValue.x, 0, MovementValue.y) * speed * Time.deltaTime;

            if (rb.velocity.magnitude >= maxVelocity)
            {
                rb.velocity = Vector3.Lerp(rb.velocity.normalized * maxVelocity, rb.velocity, 0.5f);
            }
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.Lerp(rb.velocity,Vector3.zero,slip);

        if (rb.velocity.magnitude <= BrakeLimit)
        {
            rb.velocity = Vector3.zero;
        }

    }
    
    public void DarRotacion(Vector3 RotationValue)
    {
        if (RotationValue == Vector3.zero)
        {
            if (rb.velocity.magnitude > 0)
            {
                player.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0.0f, rb.velocity.z).normalized);
            }
        }
        else
        {
            player.rotation = Quaternion.LookRotation(new Vector3(RotationValue.x, 0.0f, RotationValue.y).normalized);
        }
    }



}
