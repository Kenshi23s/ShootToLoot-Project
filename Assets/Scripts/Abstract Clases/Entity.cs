using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable, IObservable
{  
    public int life, maxLife;
    public AudioClip Aclip;
    public GameObject ParticleSample;
    public HashSet<IObserver> EntityObservers = new HashSet<IObserver>();

    private void Start()
    {
        GameManager.instance.AddEntity(this);
        StartMethod();
    }
    public abstract void StartMethod();

    #region DamageThings
    protected bool affectDamage = true;
    public void TakeDamage(int dmg)
    {
        if (affectDamage==true)
        {       
            OnTakeDamage(dmg);
            FeedBackDamage(dmg, Aclip);
            print("El objeto (" + gameObject.name + ") recibio daño");
            if (life<=0)
            {
                Die();
            }
        }
        else
        {
            print("El objeto ("+gameObject.name+") no puede recibir daño");
        }
    }

    public abstract void OnTakeDamage(int dmg);
    protected virtual void FeedBackDamage(int dmgTaken,AudioClip DmgClip)
    {
        foreach (IObserver observer in EntityObservers)
        {
            observer.Notify(life, dmgTaken, transform.position, DmgClip);
            Debug.Log(observer);
        }
        //TextPool.instance.PopUpDamageText(dmgTaken, transform.position);
        //if (ParticleSample != null)
        //{
        //    SpawnParticles(ParticleSample);
        //}
        //AudioPool.instance.SpawnAudio(DmgClip, transform.position);


    }
    public void SpawnParticles(GameObject Particles)
    {

        GameObject particlesClone = Instantiate(Particles,transform.position,transform.rotation);
        float activeTime = particlesClone.GetComponent<ParticleSystem>().main.duration;
        Destroy(particlesClone,activeTime);      
    }

    public void ChangeAffectDamage(bool ChangeAffectDamage)
    {
        affectDamage = ChangeAffectDamage;
    }
    
    public abstract void Die();
    #endregion

    private void OnDestroy()
    {
       
        GameManager.instance.RemoveEntity(this);
        OnDestroyCheck();
    }

    public abstract void OnDestroyCheck();

    public void Subscribe(IObserver elem)
    {
        EntityObservers.Add(elem);
    }

    public void Desubscribe(IObserver elem)
    {
        EntityObservers.Remove(elem);
    }
}
