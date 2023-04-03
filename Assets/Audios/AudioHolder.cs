using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    AudioSource _myAudioSource;
    Action<AudioHolder> ReturnMethod;
    public void Configure(Action<AudioHolder> ReturnMethod)
    {
        _myAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.ReturnMethod = ReturnMethod;
    }
    public void PlayClip(AudioClip clip, Vector3 WhereToPlay)
    {
        if (clip != null)
        {
            transform.position = WhereToPlay;
            _myAudioSource.PlayOneShot(clip);
            StartCoroutine(WaitForEndClip(clip.length));

        }
        else
        {
            ReturnMethod(this);
        }

    }
    IEnumerator WaitForEndClip(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnMethod(this);
    }
}
