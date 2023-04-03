using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiningManager : MonoBehaviour
{
    int randomValue, finalAngle;
    float timeInterval, totalAngle;
    bool isCoroutine = true;

    public Text winText;
    public int section;
    public string[] prizeName;
    string prize;
    //manager de spins
    public static SpiningManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isCoroutine = true;
        totalAngle = 360 / section;
    }

    public void StartSpin()
    {
        isCoroutine = true;
        totalAngle = 360 / section;
        Ads.instance.PlayAd(); //VER SI FUNCIONA EN LA APK
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        isCoroutine = false;
        randomValue = Random.Range(200, 300);

        timeInterval = 0.0001f * Time.deltaTime * 2;

        for (int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, (totalAngle / 2)); //Start rotate


            if (i > Mathf.RoundToInt(randomValue * 0.2f))
                timeInterval = 0.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 1f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.7f))
                timeInterval = 1.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.8f))
                timeInterval = 2f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randomValue * 0.9f))
                timeInterval = 2f * Time.deltaTime;

            yield return new WaitForSeconds(timeInterval);
        }

        //Cuando el indicador se detiene entre dos numeros, hace un paso adicional
        if (Mathf.RoundToInt(transform.eulerAngles.z) % totalAngle != 0)
            transform.Rotate(0, 0, totalAngle / 2);

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);


        //Prize Check
        for (int i = 0; i < section; i++)
        {
            if (finalAngle == i * totalAngle)
                winText.text = prizeName[i];
        }
        
        switch (winText.text)
        {
            case "30":
                Debug.Log("30 gemas");
                ManagerPlayerPrefs.instance.AddCurrency(30, "Gems");
                break;
            case "100":
                Debug.Log("100 monedas");
                ManagerPlayerPrefs.instance.AddCurrency(100, "Coins");
                break;
            case "120":
                Debug.Log("120 monedas");
                ManagerPlayerPrefs.instance.AddCurrency(120, "Coins");
                break;
            case "150":
                Debug.Log("150 monedas");
                ManagerPlayerPrefs.instance.AddCurrency(150, "Coins");
                break;
        }

        isCoroutine = true;
    }
}
