using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour,IObserver
{
    // lo que nesecita la pool por defecto
    #region PoolRequirements
    public PoolObject<DamageText> DamageTextPool = new PoolObject<DamageText>();
    public static TextPool instance;
    [SerializeField]
    DamageText DamageTextSample;
    Action<DamageText> ReturnMethod;
    public int prewarm = 5;
    #endregion

    #region PoolThings
    void TurnOnHolder(DamageText t)
    {
        t.gameObject.SetActive(true);
    }

    void TurnOffHolder(DamageText t)
    {
        t.gameObject.SetActive(false);
    }

    DamageText BuildHolder()
    {
        if (cameraPos == null)
        {
            cameraPos = Camera.main.transform;
        }
        DamageText dmgText = GameObject.Instantiate(DamageTextSample);
        dmgText.Configure(ReturnHolder, Camera.main);
        dmgText.transform.SetParent(this.transform);
        return dmgText;
    }
    void ReturnHolder(DamageText t)
    {
        DamageTextPool.Return(t);
    }
    DamageText GetHolder()
    {
        return DamageTextPool.Get();
    }
    #endregion


    [Range(0,1)]
    public float TextSpeed=10f;
    [Range(0, 1)]
    public float _fadeSpeed=10f;
    public int SortOrder;
       [SerializeField]
    Transform cameraPos;

    [Range(0f, 1f)]
    public float XRandomness;
    [Range(0f, 1f)]
    public float ZRandomness;
    [SerializeField]

    private void Awake()
    {
       
        DamageTextPool.Intialize(TurnOnHolder, TurnOffHolder, BuildHolder, prewarm);
        instance = this;
        

    }
   
    public void PopUpDamageText(int dmgNumber, Vector3 InitialPos)
    {
        DamageText t = GetHolder();

        if (t != null)
        {
            TurnOnHolder(t);
            t.SetupText(dmgNumber, InitialPos, XRandomness, ZRandomness);
            
        }
    }

   

    public void Notify()
    {
        return;
    }

    public void Notify(int ActualLife, int DamageTaken, Vector3 Where, AudioClip clip)
    {
        //Debug.Log("notify de TextPool");
        PopUpDamageText(DamageTaken, Where);
    }
}
