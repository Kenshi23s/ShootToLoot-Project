using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDconfig : MonoBehaviour
{
    public static HUDconfig instance;

    public List<Image> CanvasImage = new List<Image>();

    [Range(0f, 1f)]
    public float Opacity = 1;
    public bool AcceptChange = false;
    public Text CoinsUI, GemsUI;
    public Slider LifeSlider;
  
   
    public GameObject GameUI, PauseUI;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OnPauseChange(false);
        ChangeAllOpacity();
    }
   
    void Update()
    {
        if (AcceptChange)
        {
            ChangeAllOpacity();
            AcceptChange = false;
        }
        
    }

    public void OnPauseChange(bool Pause)
    {
        if (Pause==true)
        {
            Time.timeScale = 0;
            GameUI.SetActive(false);
            PauseUI.SetActive(true);

        }
        else
        {
            Time.timeScale = 1;
            GameUI.SetActive(true);
            PauseUI.SetActive(false);
        }
    }

    public void ChangeAllOpacity()
    {
        for (int i = 0; i < CanvasImage.Count; i++)
        {
            ChangeOpacity(i,Opacity);
        }      
    }

    public void ChangeOpacity(int select, float Opacity)
    {
        CanvasImage[select].color = new Color(255,255,255,Opacity);
        //print("la opacidad de (" + CanvasImage[select].name + ") casilla[" + select + "] es: " +Opacity);
    }

    public void CoinsUIUpdate(int actualCoins)
    {
        CoinsUI.text = actualCoins.ToString();
    }

    public void GemsUIUpdate(int actualGems)
    {
        GemsUI.text = actualGems.ToString();
    }

    public void ChangeLifeSlider(int Life)
    {
        LifeSlider.value = Life;
    }

  

    public void Notify()
    {
        throw new System.NotImplementedException();
    }
}
