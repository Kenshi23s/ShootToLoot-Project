using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour, IUnityAdsShowListener
{
    public int rewardGems = 100;
    string _androidIdGame = "5006357";
    public static Ads instance;

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Doy 100 gemas por ver publicidad");
        //Aca sumar gemas al playerPrefs
        ManagerPlayerPrefs.instance.AddCurrency(rewardGems, "Gems");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("No vio la publicidad, a casa, no le doy nada");
        //Mostrar cartel de error
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Empezó la publicidad");
        //Parar la música del juego
    }

    private void Awake()
    {
        instance = this;

        #if UNITY_EDITOR
        Advertisement.Initialize(_androidIdGame, true);
        #elif UNITY_ANDROID
        Advertisement.Initialize(_androidIdGame, false);
        #endif
    }

    public void PlayAd()
    {
        if(Advertisement.IsReady("Rewarded_Android"))
           Advertisement.Show("Rewarded_Android",this);
    }

    
}
