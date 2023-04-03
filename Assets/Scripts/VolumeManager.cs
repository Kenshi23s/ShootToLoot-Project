using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.gameObject.GetComponent<Slider>();
        Initialize();
    }
    void Initialize()
    {
        if (PlayerPrefs.HasKey("UserVolume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("UserVolume");
            AudioListener.volume = volumeSlider.value;
        }
        else
        {
            PlayerPrefs.SetFloat("UserVolume", 1);
            Initialize();
        }
    }
    public void ChangeVolume(float value)
    {
        value=Mathf.Clamp01(value);
        PlayerPrefs.SetFloat("UserVolume", value);
        AudioListener.volume = value;

    }
    
}
