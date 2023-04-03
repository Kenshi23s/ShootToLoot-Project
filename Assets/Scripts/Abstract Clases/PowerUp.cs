using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public abstract void PowerUpTrigger(PlayerEntity player);
    [SerializeField]
    AudioClip PowerUpGrab;
    [SerializeField]
    GameObject GrabParticle;
    float particleDuration;

    private void Start()
    {
        particleDuration = GrabParticle.GetComponent<ParticleSystem>().main.duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerEntity>();
        if (player != null)
        {
            PowerUpTrigger(player);
            Feedback(player.SpawnParticles);
        }
    }
    void Feedback(Action<GameObject> particlemethod)
    {       
        AudioPool.instance.SpawnAudio(PowerUpGrab, transform.position);
        if (GrabParticle != null)
        {
            particlemethod.Invoke(GrabParticle);
        }           
        
    }
}




