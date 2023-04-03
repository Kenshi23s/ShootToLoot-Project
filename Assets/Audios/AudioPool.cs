using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour,IObserver
{
    public PoolObject<AudioHolder> AudioHoldersPool = new PoolObject<AudioHolder>();
    public AudioHolder AudioHolderSample;
    public static AudioPool instance;
    public int prewarm=5;
    Action<AudioHolder> ReturnMethod;

    private void Awake()
    {
        AudioHoldersPool.Intialize(TurnOnHolder, TurnOffHolder, BuildHolder, prewarm);
        instance = this;      
    }
    public void SpawnAudio(AudioClip clip,Vector3 WhereToPlay)
    {
       
        AudioHolder A = GetHolder();

        if (A != null&& clip != null)
        {
            TurnOnHolder(A);
            A.PlayClip(clip, WhereToPlay);
        }
        else
        {
            Debug.LogWarning(" el objeto (" + gameObject.name + ") trato de ejecutar un audio no existente O el audio holder es igual a null :( ");
        }
    }


    void TurnOnHolder(AudioHolder a)
    {
        a.gameObject.SetActive(true);
    }

    void TurnOffHolder(AudioHolder a)
    {
        a.gameObject.SetActive(false);
    }

    AudioHolder BuildHolder()
    {
        AudioHolder audio = GameObject.Instantiate(AudioHolderSample);
        audio.Configure(ReturnHolder);
        audio.transform.SetParent(this.transform);
        return audio;
    }

    void ReturnHolder(AudioHolder a)
    {
        AudioHoldersPool.Return(a);
    }

    AudioHolder GetHolder()
    {
        return AudioHoldersPool.Get();
    }

    public void Notify(int ActualLife, int DamageTaken, Vector3 Where, AudioClip clip)
    {
        SpawnAudio(clip, Where);
    }

    public void Notify()
    {
        throw new NotImplementedException();
    }
}
