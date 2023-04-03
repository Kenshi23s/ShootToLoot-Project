using System;
using UnityEngine;


public class StaminaPlay: MonoBehaviour
{
    //esta clase se deberia crear en el gamemanager
    public StaminaPlay(int maxStamina, int stamina, int minusStamina)
    {
        _maxStamina = maxStamina;
        _stamina = stamina;
        _minusStamina = minusStamina;
    }
    [SerializeField]
     int _maxStamina;
     public int _stamina;
    [SerializeField]
    int _minusStamina;

    private void Start()
    {
        StaminaStart();
    }
    public void StaminaStart()
    {
        if (PlayerPrefs.HasKey("Stamina"))
        {
            _stamina = PlayerPrefs.GetInt("Stamina");
            if (PlayerPrefs.HasKey("Time"))
            {
                // parse convierte un string a un tipo de dato "DateTime"
                DateTime time = DateTime.Parse(PlayerPrefs.GetString("Time"));

                var actualStamina = PlayerPrefs.GetInt("Stamina");

                int multiply = 0;

                for (int i = actualStamina; i <= _maxStamina; i++)
                {
                    multiply++;
                    if (DateTime.Compare(DateTime.Now, time.AddMinutes(multiply * 2)) >= 0)
                    {
                        _stamina++;
                    }
                }
            
                 if (_stamina >= _maxStamina)
                 {
                     _stamina = _maxStamina;
                     PlayerPrefs.DeleteKey("Time");
                 }
            }
        }
        else
        {
            PlayerPrefs.SetInt("Stamina", _maxStamina);
            Debug.Log("stamina no existia");
            StaminaStart();
            return;

        }
        if (MenuManager.instance!=null)
        {
            MenuManager.instance.SetSliderMaxValue(_maxStamina);
            MenuManager.instance.ChangeSliderValue(_stamina);
        }
        

    }
 
    public void StaminaUpdate()
    {
        if (PlayerPrefs.HasKey("Time"))
        {
            DateTime time = DateTime.Parse(PlayerPrefs.GetString("Time"));

             //si el tiempo que paso es mayor o igual a 0 
            if (DateTime.Compare(DateTime.Now, time) >= 0)
            {
                //sumo stamina
                _stamina++;
                //si la stamina es mayor o igual a la maxima stamina maxima
                if (_stamina >= _maxStamina)
                {
                    //borro el valor de tiempo y su llave
                    PlayerPrefs.DeleteKey("Time");
                }// y si no es asi,
                else
                    PlayerPrefs.SetString("Time", DateTime.Now.AddSeconds(10).ToString());

                PlayerPrefs.SetInt("Stamina", _stamina);
            }

        }
      
        MenuManager.instance.ChangeSliderValue(_stamina);
    }

    public bool OnPlayButton()
    {
        if (_stamina>=_minusStamina)
        {
            _stamina -= _minusStamina;

            PlayerPrefs.SetInt("Stamina", _stamina);

            if (_stamina !> _maxStamina)
            {
                PlayerPrefs.SetString("Time", DateTime.Now.AddSeconds(10).ToString());
            }
            return true;
        }
        else
        {
            return false;
        }
        

    }
}
