using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using System;

public class DamageText : MonoBehaviour
{
     public TextMeshPro _DmgText;
    float _Xmove;
    float _Zmove;
    float _speed;
    Vector3 _TextForce;
    Vector3 _initialPos;
    
    float _FadeSpeed;
    Action<DamageText> ReturnMethod;

    Camera camPos;

    void LookCamera(Vector3 cam, Vector3 posToChange)
    {
        Vector3 v = new Vector3(posToChange.x,cam.y,cam.z);
        
        transform.LookAt(v);
    }

   
    //Al crearse
    public void Configure(Action<DamageText> ReturnMethod,Camera camPos)
    {
        
        Vector3 TextForce = Vector3.zero;
        this.camPos = camPos;
        // MI metodo Return es igual al metodo Return que me pasaron por parametro
        this.ReturnMethod = ReturnMethod;
        
    }
    /// <summary>
    ///asigna un valor random a un numero entre -Randomness y Randomness
    /// </summary>
    /// <param name="number"></param>
    /// <param name="Randomness"></param>
    /// <returns></returns>
    float SetRandomValue(float number, float Randomness)
    {
        Randomness = Mathf.Abs(Randomness);
        //pongo UnityEngine porque me hacia interferencia con el random de la libreria SYSTEM
        number = UnityEngine.Random.Range(-Randomness, Randomness);
        return number;
    }

    /// <summary>
    /// este metodo es el encargado de preparar a el texto para que haga todos los comportamientos necesarios para su correcta funcion
    /// </summary>
    /// <param name="TextValueDamage"></param>
    /// <param name="pos"></param>
    /// <param name="XRandomSpread"></param>
    /// <param name="ZRandomSpread"></param>
    public void SetupText(int TextValueDamage, Vector3 pos, float XRandomSpread, float ZRandomSpread)
    {
        //inital pos lo uso para fijarme(en el gizmos) en que direccion va el texto 
       _initialPos = pos;
       transform.position = pos;
       PrepareText(TextValueDamage);   
       SetForce(XRandomSpread, ZRandomSpread);
    }
    void PrepareText(int value)
    {
        // esto es para que el texto actual tenga prioridad de renderizado sobre los anteriores
        TextPool.instance.SortOrder++;
        _DmgText.sortingOrder = TextPool.instance.SortOrder;

        //aplico el int pasado al texto
        _DmgText.text = value.ToString();
        // se pone el alpha al maximo por las dudas de que no lo estuviera
        _DmgText.alpha = 255f;
        _DmgText.gameObject.name = ("Text Damage  " + value );
    }
    void SetForce(float RandomX,float RandomZ)
    {
        _Xmove = SetRandomValue(_Xmove, RandomX);
        _Zmove = SetRandomValue(_Zmove, RandomZ);
        _TextForce = new Vector3(_Xmove,0.1F, _Zmove);
    }

    void Update()
    {
        //se le suma al transform una fuerza para que se mueva a lo largo del tiempo hasta que el "Alpha" del texto llegue a 0

        this.transform.position += _TextForce.normalized * Time.deltaTime * 5;
        float A = SubstractAlpha(TextPool.instance._fadeSpeed);
        if (A == 0)
        {
            GoToPool();
        }
    }

    private void LateUpdate()
    {
        LookCamera(Camera.main.transform.position,transform.position);
    }
    float SubstractAlpha(float Decreasespeed)
    {
        _DmgText.alpha = Mathf.Clamp(_DmgText.alpha, 0f, 1f);
        _DmgText.alpha -= Decreasespeed* Time.deltaTime;
        _DmgText.alpha = Mathf.Clamp(_DmgText.alpha, 0f, 1f);
        float t = _DmgText.alpha;
        return t;

       
    }
    void GoToPool()
    {
        _TextForce = Vector3.zero;
        _initialPos = Vector3.zero;
        ReturnMethod(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_initialPos, _initialPos+_TextForce);
    }
}
