using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ManagerPlayerPrefs : MonoBehaviour
{
    int _coins, _gems, touch;
    public float time=2;
    public Text uiCoins, uiGems;
    public GameObject tutorial;
    [SerializeField] GameObject GemsObject, CoinsObject;
    
    public Text deleteText;
    public int touchsBeforeDelete=2;
    public static ManagerPlayerPrefs instance;

    [SerializeField] int MaxStamina;
    [SerializeField] int actualStamina;
    [SerializeField] int minusStamina;
    [NonSerialized]
    public StaminaPlay StaminaManager;
    public static Action UpdateCurrencys;

    private void Awake()
    {
       StaminaManager = new StaminaPlay(MaxStamina, actualStamina, minusStamina);   
       Initialize();
       instance = this;
       UpdateCurrencys = UpdateCurrencyShow;
    }
    private void Start()
    {
        StaminaManager.StaminaStart();
        StaminaManager.StaminaUpdate();
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            _coins = PlayerPrefs.GetInt("Coins");
            uiCoins.text = _coins.ToString();
        }
        else
        {
            tutorial.SetActive(true);
            PlayerPrefs.SetInt("Coins", 0);
            _coins = PlayerPrefs.GetInt("Coins");
            uiCoins.text = _coins.ToString();
        }

        _gems = PlayerPrefs.GetInt("Gems");
        uiGems.text = _gems.ToString();
    }

    private void UpdateCurrencyShow()
    {
        _coins = PlayerPrefs.GetInt("Coins");
        uiCoins.text = _coins.ToString();

        _gems = PlayerPrefs.GetInt("Gems");
        uiGems.text = _gems.ToString();
    }

    public void AddCurrency(int valueCurrency, string key)
    {
        int actualValue;
        _coins += valueCurrency;

        if(PlayerPrefs.HasKey(key) != false)
        {
            actualValue = PlayerPrefs.GetInt(key);
            actualValue += valueCurrency;
            PlayerPrefs.SetInt(key, actualValue);
            UpdateCurrencyShow();
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
            AddCurrency(valueCurrency, key);
        }

    }

    public void SubstractCurrency(int valueCurrency, string key)
    {
        int actualValue;
        _coins += valueCurrency;

        if (PlayerPrefs.HasKey(key) != false)
        {
            actualValue = PlayerPrefs.GetInt(key);
            actualValue -= valueCurrency;
            PlayerPrefs.SetInt(key, actualValue);
            UpdateCurrencyShow();
        }
        else
        {
            PlayerPrefs.SetInt(key,0);  
            SubstractCurrency(valueCurrency, key);
        }

    }

    //public void ChangeM4Skin(string skin, string key)
    //{
    //    PlayerPrefs.SetString(key, skin);
    //}

    #region deleteData
    // ponele esto al button
    public void DeleteData()
    {
        touch++;
        deleteText.text = "Delete data?";
        if (touch > touchsBeforeDelete)
        {
            touch = 0;
            ConfirmDeleteData();
        }
    }

    void ConfirmDeleteData()
    {
        PlayerPrefs.DeleteAll();
        deleteText.text = "Deleted!!!!!!";
        StartCoroutine(Wait(time));
       
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        deleteText.text = "Delete Data";
        Initialize();
    }
    #endregion

}
